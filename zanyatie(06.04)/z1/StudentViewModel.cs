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
        private GradeService _gradeService;
        private ObservableCollection<StudentModel> _students;
        private ObservableCollection<GradeModel> _allGrades;
        private ObservableCollection<GradeModel> _selectedStudentGrades;
        private StudentModel _selectedStudent;
        private double _selectedStudentAverageGrade;
        private bool _isLoading;

        public StudentViewModel()
        {
            _gradeService = new GradeService();
            _students = new ObservableCollection<StudentModel>();
            _allGrades = new ObservableCollection<GradeModel>();
            _selectedStudentGrades = new ObservableCollection<GradeModel>();

            AddGradeCommand = new RelayCommand(AddGrade, CanAddGrade);
            LoadDataCommand = new RelayCommand(async () => await LoadDataAsync());
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

        public StudentModel SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
                LoadGradesForSelectedStudent();
                UpdateAverageGrade();
                ((RelayCommand)AddGradeCommand).RaiseCanExecuteChanged();
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

        public ICommand AddGradeCommand { get; }
        public ICommand LoadDataCommand { get; }

        public async Task LoadDataAsync()
        {
            IsLoading = true;

            Students = await _gradeService.LoadStudentsAsync();
            _allGrades = await _gradeService.LoadGradesAsync();

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

        private void AddGrade()
        {
            var window = new GradeWindow(SelectedStudent.FullName);

            if (window.ShowDialog() == true)
            {
                _gradeService.AddGrade(_allGrades, SelectedStudent.Id, window.Subject, window.Grade);
                LoadGradesForSelectedStudent();
                UpdateAverageGrade();
            }
        }

        private bool CanAddGrade()
        {
            return SelectedStudent != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}