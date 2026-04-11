using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace z1
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        private readonly GradeService _gradeService;
        private ObservableCollection<StudentModel> _students;
        private ObservableCollection<GradeModel> _allGrades;
        private ObservableCollection<GradeModel> _selectedStudentGrades;
        private StudentModel? _selectedStudent;
        private double _selectedStudentAverageGrade;
        private bool _isLoading;
        private readonly UserModel _currentUser;

        public StudentViewModel(UserModel currentUser)
        {
            _currentUser = currentUser;
            _gradeService = new GradeService();
            _students = new ObservableCollection<StudentModel>();
            _allGrades = new ObservableCollection<GradeModel>();
            _selectedStudentGrades = new ObservableCollection<GradeModel>();

            AddGradeCommand = new DelegateCommand(async () => await AddGradeAsync(), CanAddGrade);
            DeleteGradeCommand = new DelegateCommand(async () => await DeleteGradeAsync(), CanDeleteGrade);
            LoadDataCommand = new DelegateCommand(async () => await LoadDataAsync());
            OpenChatCommand = new DelegateCommand(OpenChat);
            OpenAssignmentsCommand = new DelegateCommand(OpenAssignments);
        }

        public ObservableCollection<StudentModel> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<GradeModel> SelectedStudentGrades
        {
            get { return _selectedStudentGrades; }
            set
            {
                _selectedStudentGrades = value;
                OnPropertyChanged();
            }
        }

        public StudentModel? SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
                LoadGradesForSelectedStudent();
                UpdateAverageGrade();
                ((DelegateCommand)AddGradeCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)DeleteGradeCommand).RaiseCanExecuteChanged();
            }
        }

        public double SelectedStudentAverageGrade
        {
            get { return _selectedStudentAverageGrade; }
            set
            {
                _selectedStudentAverageGrade = value;
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

        public UserModel CurrentUser
        {
            get { return _currentUser; }
        }

        public bool CanEditGrades
        {
            get { return _currentUser.Role == "Teacher"; }
        }

        public ICommand AddGradeCommand { get; }
        public ICommand DeleteGradeCommand { get; }
        public ICommand LoadDataCommand { get; }
        public ICommand OpenChatCommand { get; }
        public ICommand OpenAssignmentsCommand { get; }

        public async Task LoadDataAsync()
        {
            IsLoading = true;

            Students = await _gradeService.LoadStudentsAsync();
            _allGrades = await _gradeService.LoadGradesAsync();

            if (Students.Count == 0)
            {
                var defaultStudents = new[]
                {
                    new StudentModel { Id = 1, FullName = "Иван Иванов", UserId = 2 },
                    new StudentModel { Id = 2, FullName = "Петр Петров", UserId = 0 },
                    new StudentModel { Id = 3, FullName = "Анна Сидорова", UserId = 0 }
                };

                foreach (var s in defaultStudents)
                {
                    Students.Add(s);
                }
            }

            if (_allGrades.Count == 0 && Students.Count > 0)
            {
                var defaultGrades = new[]
                {
                    new GradeModel { Id = 1, StudentId = 1, Subject = "Математика", GradeValue = 8, Date = System.DateTime.Now.AddDays(-10) },
                    new GradeModel { Id = 2, StudentId = 1, Subject = "Физика", GradeValue = 7, Date = System.DateTime.Now.AddDays(-8) },
                    new GradeModel { Id = 3, StudentId = 2, Subject = "Математика", GradeValue = 4, Date = System.DateTime.Now.AddDays(-7) },
                    new GradeModel { Id = 4, StudentId = 3, Subject = "Математика", GradeValue = 10, Date = System.DateTime.Now.AddDays(-6) }
                };

                foreach (var g in defaultGrades)
                {
                    _allGrades.Add(g);
                }
            }

            if (_currentUser.Role == "Student" && _currentUser.Id == 2)
            {
                var currentStudent = Students.FirstOrDefault(s => s.UserId == _currentUser.Id);
                if (currentStudent != null)
                {
                    SelectedStudent = currentStudent;
                }
            }

            IsLoading = false;
        }

        private void LoadGradesForSelectedStudent()
        {
            SelectedStudentGrades.Clear();

            if (SelectedStudent == null) return;

            var gradesForStudent = _allGrades.Where(g => g.StudentId == SelectedStudent.Id).ToList();

            foreach (var grade in gradesForStudent)
            {
                SelectedStudentGrades.Add(grade);
            }
        }

        private void UpdateAverageGrade()
        {
            SelectedStudentAverageGrade = _gradeService.GetAverageGrade(SelectedStudentGrades);
        }

        private async Task AddGradeAsync()
        {
            if (!CanEditGrades) return;
            if (SelectedStudent == null) return;

            var window = new GradeWindow(SelectedStudent.FullName);

            if (window.ShowDialog() == true)
            {
                await _gradeService.AddGrade(_allGrades, SelectedStudent.Id, window.Subject, window.Grade);
                LoadGradesForSelectedStudent();
                UpdateAverageGrade();
            }
        }

        private bool CanAddGrade()
        {
            return SelectedStudent != null && CanEditGrades;
        }

        private async Task DeleteGradeAsync()
        {
            if (SelectedStudentGrades.Count == 0) return;

            var lastGrade = SelectedStudentGrades.LastOrDefault();
            if (lastGrade == null) return;

            var result = MessageBox.Show(
                $"Удалить оценку {lastGrade.GradeValue} по предмету {lastGrade.Subject}?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                await _gradeService.DeleteGrade(lastGrade, _allGrades, SelectedStudentGrades);
                UpdateAverageGrade();
            }
        }

        private bool CanDeleteGrade()
        {
            return SelectedStudentGrades.Count > 0 && CanEditGrades;
        }

        private void OpenChat()
        {
            var chatWindow = new ChatWindow(_currentUser.FullName);
            chatWindow.Show();
        }

        private void OpenAssignments()
        {
            var assignmentWindow = new AssignmentWindow(_currentUser);
            assignmentWindow.Show();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}