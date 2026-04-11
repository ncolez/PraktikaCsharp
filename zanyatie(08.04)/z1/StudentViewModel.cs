using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace z1
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        private readonly GradeService _gradeService = new GradeService();
        private readonly DataService _dataService = new DataService();
        private ObservableCollection<StudentModel> _students = new ObservableCollection<StudentModel>();
        private ObservableCollection<GradeModel> _allGrades = new ObservableCollection<GradeModel>();
        private ObservableCollection<GradeModel> _selectedStudentGrades = new ObservableCollection<GradeModel>();
        private StudentModel? _selectedStudent;
        private double _selectedStudentAverageGrade;
        private bool _isLoading;
        private readonly UserModel _currentUser;

        public event System.Action<GradeModel>? GradeAdded;

        public StudentViewModel(UserModel currentUser)
        {
            _currentUser = currentUser;

            AddGradeCommand = new DelegateCommand(AddGrade, CanAddGrade);
            LoadDataCommand = new DelegateCommand(async () => await LoadDataAsync());
            SaveDataCommand = new DelegateCommand(async () => await SaveDataAsync());
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
        public ICommand LoadDataCommand { get; }
        public ICommand SaveDataCommand { get; }
        public ICommand OpenChatCommand { get; }
        public ICommand OpenAssignmentsCommand { get; }

        public async Task LoadDataAsync()
        {
            IsLoading = true;

            var (students, grades) = await _dataService.LoadDiaryDataAsync();

            if (students.Count == 0)
            {
                students.Add(new StudentModel { Id = 1, FullName = "Иван Иванов", UserId = 2 });
                students.Add(new StudentModel { Id = 2, FullName = "Петр Петров", UserId = 0 });
                students.Add(new StudentModel { Id = 3, FullName = "Анна Сидорова", UserId = 0 });
            }

            if (grades.Count == 0 && students.Count > 0)
            {
                grades.Add(new GradeModel { Id = 1, StudentId = 1, Subject = "Математика", GradeValue = 8, Date = System.DateTime.Now.AddDays(-10) });
                grades.Add(new GradeModel { Id = 2, StudentId = 1, Subject = "Физика", GradeValue = 7, Date = System.DateTime.Now.AddDays(-8) });
                grades.Add(new GradeModel { Id = 3, StudentId = 2, Subject = "Математика", GradeValue = 4, Date = System.DateTime.Now.AddDays(-7) });
                grades.Add(new GradeModel { Id = 4, StudentId = 3, Subject = "Математика", GradeValue = 10, Date = System.DateTime.Now.AddDays(-6) });
            }

            _students = students;
            _allGrades = grades;
            Students = _students;

            if (_currentUser.Role == "Student" && _currentUser.Id == 2)
            {
                var currentStudent = _students.FirstOrDefault(s => s.UserId == _currentUser.Id);
                if (currentStudent != null)
                {
                    SelectedStudent = currentStudent;
                }
            }

            IsLoading = false;
        }

        public async Task SaveDataAsync()
        {
            await _dataService.SaveDiaryDataAsync(_students, _allGrades);
        }

        private void LoadGradesForSelectedStudent()
        {
            SelectedStudentGrades.Clear();

            if (SelectedStudent == null)
            {
                return;
            }

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

        private void AddGrade()
        {
            if (!CanEditGrades)
            {
                return;
            }

            if (SelectedStudent == null)
            {
                return;
            }

            var window = new GradeWindow(SelectedStudent.FullName);

            if (window.ShowDialog() == true)
            {
                var newGrade = new GradeModel
                {
                    Id = _allGrades.Count + 1,
                    StudentId = SelectedStudent.Id,
                    Subject = window.Subject,
                    GradeValue = window.Grade,
                    Date = System.DateTime.Now
                };

                _gradeService.AddGrade(_allGrades, SelectedStudent.Id, window.Subject, window.Grade);
                LoadGradesForSelectedStudent();
                UpdateAverageGrade();
                _ = SaveDataAsync();

                GradeAdded?.Invoke(newGrade);
            }
        }

        private bool CanAddGrade()
        {
            return SelectedStudent != null && CanEditGrades;
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