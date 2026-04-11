using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using z1.Models;
using z1.Services;

namespace z1.ViewModels
{
    public class AssignmentViewModel : INotifyPropertyChanged
    {
        private readonly AssignmentService _assignmentService;
        private readonly NotificationService _notificationService;
        private ObservableCollection<AssignmentModel> _allAssignments;
        private ObservableCollection<AssignmentModel> _filteredAssignments;
        private string _selectedSubject;
        private AssignmentModel? _selectedAssignment;
        private UserModel _currentUser;
        private bool _isLoading;

        public ObservableCollection<string> Subjects { get; } = new ObservableCollection<string>
        {
            "Математика",
            "Физика",
            "Программирование",
            "Английский язык",
            "Русский язык",
            "Литература",
            "Химия",
            "Биология",
            "История"
        };

        public ObservableCollection<AssignmentModel> FilteredAssignments
        {
            get { return _filteredAssignments; }
            set
            {
                _filteredAssignments = value;
                OnPropertyChanged();
            }
        }

        public string SelectedSubject
        {
            get { return _selectedSubject; }
            set
            {
                _selectedSubject = value;
                OnPropertyChanged();
                FilterAssignments();
            }
        }

        public AssignmentModel? SelectedAssignment
        {
            get { return _selectedAssignment; }
            set
            {
                _selectedAssignment = value;
                OnPropertyChanged();
                ((DelegateCommand)EditAssignmentCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)DeleteAssignmentCommand).RaiseCanExecuteChanged();
            }
        }

        public UserModel CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public bool IsTeacher
        {
            get { return _currentUser != null && _currentUser.Role == "Teacher"; }
        }

        public ICommand AddAssignmentCommand { get; }
        public ICommand EditAssignmentCommand { get; }
        public ICommand DeleteAssignmentCommand { get; }
        public ICommand LoadDataCommand { get; }

        public AssignmentViewModel(UserModel currentUser)
        {
            _currentUser = currentUser;
            _assignmentService = new AssignmentService();
            _notificationService = new NotificationService();
            _allAssignments = new ObservableCollection<AssignmentModel>();
            _filteredAssignments = new ObservableCollection<AssignmentModel>();
            _selectedSubject = string.Empty;

            AddAssignmentCommand = new DelegateCommand(AddAssignment, () => IsTeacher);
            EditAssignmentCommand = new DelegateCommand(EditAssignment, () => IsTeacher && SelectedAssignment != null);
            DeleteAssignmentCommand = new DelegateCommand(DeleteAssignment, () => IsTeacher && SelectedAssignment != null);
            LoadDataCommand = new DelegateCommand(async () => await LoadDataAsync());
        }

        public async Task LoadDataAsync()
        {
            IsLoading = true;
            _allAssignments = await _assignmentService.LoadAssignmentsAsync();
            FilterAssignments();
            IsLoading = false;
        }

        private void FilterAssignments()
        {
            FilteredAssignments.Clear();

            if (string.IsNullOrEmpty(SelectedSubject))
            {
                return;
            }

            var filtered = _allAssignments.Where(a => a.Subject == SelectedSubject).ToList();

            foreach (var assignment in filtered)
            {
                FilteredAssignments.Add(assignment);
            }
        }

        private async void AddAssignment()
        {
            var window = new AssignmentEditWindow(null, Subjects.ToList());

            if (window.ShowDialog() == true)
            {
                _assignmentService.AddAssignment(
                    _allAssignments,
                    window.AssignmentSubject,
                    window.AssignmentTitle,
                    window.AssignmentDescription,
                    window.AssignmentDueDate,
                    _currentUser.Id);

                await _assignmentService.SaveAssignmentsAsync(_allAssignments);
                FilterAssignments();

                _notificationService.SendNotification(
                    $"Новое задание: {window.AssignmentTitle} по предмету {window.AssignmentSubject}",
                    "Student");
            }
        }

        private async void EditAssignment()
        {
            if (SelectedAssignment == null) return;

            var window = new AssignmentEditWindow(SelectedAssignment, Subjects.ToList());

            if (window.ShowDialog() == true)
            {
                _assignmentService.UpdateAssignment(
                    SelectedAssignment,
                    window.AssignmentTitle,
                    window.AssignmentDescription,
                    window.AssignmentDueDate);

                await _assignmentService.SaveAssignmentsAsync(_allAssignments);
                FilterAssignments();
            }
        }

        private async void DeleteAssignment()
        {
            if (SelectedAssignment == null) return;

            var result = MessageBox.Show(
                $"Удалить задание \"{SelectedAssignment.Title}\"?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _assignmentService.DeleteAssignment(_allAssignments, SelectedAssignment);
                await _assignmentService.SaveAssignmentsAsync(_allAssignments);
                FilterAssignments();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}