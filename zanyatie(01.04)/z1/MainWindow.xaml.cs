using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace z1
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<GradeItem> _grades;
        private double _averageGrade;

        public ObservableCollection<GradeItem> Grades
        {
            get { return _grades; }
            set
            {
                _grades = value;
                OnPropertyChanged(nameof(Grades));
            }
        }

        public double AverageGrade
        {
            get { return _averageGrade; }
            set
            {
                _averageGrade = value;
                OnPropertyChanged(nameof(AverageGrade));
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            _grades = new ObservableCollection<GradeItem>();
            _grades.Add(new GradeItem { StudentName = "Иван Иванов", Subject = "Математика", Grade = 5, Date = "01.03.2025" });
            _grades.Add(new GradeItem { StudentName = "Иван Иванов", Subject = "Физика", Grade = 4, Date = "02.03.2025" });
            _grades.Add(new GradeItem { StudentName = "Петр Петров", Subject = "Математика", Grade = 3, Date = "03.03.2025" });

            Grades = _grades;
            DataContext = this;
            RecalculateAverage();
        }

        public ICommand AddGradeCommand
        {
            get { return new RelayCommand(AddGrade); }
        }

        public ICommand DeleteGradeCommand
        {
            get { return new RelayCommand(DeleteGrade, CanDeleteGrade); }
        }

        public ICommand UpdateAverageCommand
        {
            get { return new RelayCommand(RecalculateAverage); }
        }

        private void AddGrade()
        {
            var window = new GradeWindow();

            if (window.ShowDialog() == true)
            {
                var newGrade = new GradeItem
                {
                    StudentName = window.StudentName,
                    Subject = window.Subject,
                    Grade = window.Grade,
                    Date = DateTime.Now.ToString("dd.MM.yyyy")
                };

                Grades.Add(newGrade);
                RecalculateAverage();
            }
        }

        private void DeleteGrade()
        {
            if (Grades.Count > 0)
            {
                var lastItem = Grades[Grades.Count - 1];
                Grades.Remove(lastItem);
                RecalculateAverage();
            }
        }

        private bool CanDeleteGrade()
        {
            return Grades.Count > 0;
        }

        private void RecalculateAverage()
        {
            if (Grades.Count == 0)
            {
                AverageGrade = 0;
                return;
            }

            double sum = 0;

            foreach (var grade in Grades)
            {
                sum += grade.Grade;
            }

            AverageGrade = Math.Round(sum / Grades.Count, 2);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}