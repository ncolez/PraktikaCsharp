using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace z1
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<GradeItem> Grades { get; set; }
        public ObservableCollection<ScheduleItem> Schedule { get; set; }
        public GradeItem SelectedGrade { get; set; }

        public ICommand AddGradeCommand { get; }
        public ICommand EditGradeCommand { get; }
        public ICommand DeleteGradeCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand ShowScheduleCommand { get; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            AddGradeCommand = new RelayCommand(AddGrade);
            EditGradeCommand = new RelayCommand(EditGrade, () => SelectedGrade != null);
            DeleteGradeCommand = new RelayCommand(DeleteGrade, () => SelectedGrade != null);
            ExitCommand = new RelayCommand(Exit);
            ShowScheduleCommand = new RelayCommand(ShowSchedule);

            LoadSchedule();
            LoadGrades();
        }

        private void LoadSchedule()
        {
            Schedule = new ObservableCollection<ScheduleItem>();

            Schedule.Add(new ScheduleItem { LessonNumber = 1, Monday = "Физика", Tuesday = "Математика", Wednesday = "Русский язык", Thursday = "Английский", Friday = "История" });
            Schedule.Add(new ScheduleItem { LessonNumber = 2, Monday = "Математика", Tuesday = "Физика", Wednesday = "Английский", Thursday = "Математика", Friday = "Литература" });
            Schedule.Add(new ScheduleItem { LessonNumber = 3, Monday = "Русский язык", Tuesday = "Химия", Wednesday = "Математика", Thursday = "Физкультура", Friday = "Математика" });
            Schedule.Add(new ScheduleItem { LessonNumber = 4, Monday = "Английский", Tuesday = "Биология", Wednesday = "Физика", Thursday = "Русский язык", Friday = "ОБЖ" });
            Schedule.Add(new ScheduleItem { LessonNumber = 5, Monday = "История", Tuesday = "Русский язык", Wednesday = "Литература", Thursday = "Химия", Friday = "Физика" });
            Schedule.Add(new ScheduleItem { LessonNumber = 6, Monday = "", Tuesday = "", Wednesday = "Физкультура", Thursday = "", Friday = "" });

            ScheduleGrid.ItemsSource = Schedule;
        }

        private void LoadGrades()
        {
            Grades = new ObservableCollection<GradeItem>();
            Grades.Add(new GradeItem { Subject = "Математика", Grade = 5, Date = "01.03.2025" });
            Grades.Add(new GradeItem { Subject = "Математика", Grade = 4, Date = "05.03.2025" });
            Grades.Add(new GradeItem { Subject = "Физика", Grade = 4, Date = "02.03.2025" });
            Grades.Add(new GradeItem { Subject = "Программирование", Grade = 5, Date = "03.03.2025" });
            Grades.Add(new GradeItem { Subject = "Английский", Grade = 3, Date = "04.03.2025" });
            Grades.Add(new GradeItem { Subject = "Русский язык", Grade = 4, Date = "06.03.2025" });
            Grades.Add(new GradeItem { Subject = "Литература", Grade = 5, Date = "07.03.2025" });
            Grades.Add(new GradeItem { Subject = "Химия", Grade = 4, Date = "08.03.2025" });
            Grades.Add(new GradeItem { Subject = "Биология", Grade = 5, Date = "09.03.2025" });
            Grades.Add(new GradeItem { Subject = "Физкультура", Grade = 5, Date = "10.03.2025" });
            Grades.Add(new GradeItem { Subject = "ОБЖ", Grade = 4, Date = "11.03.2025" });
            GradesGrid.ItemsSource = Grades;
        }

        private void AddGrade()
        {
            GradeWindow window = new GradeWindow();
            if (window.ShowDialog() == true)
            {
                Grades.Add(new GradeItem
                {
                    Subject = window.Subject,
                    Grade = window.Grade,
                    Date = window.Date
                });
            }
        }

        private void EditGrade()
        {
            if (SelectedGrade != null)
            {
                GradeWindow window = new GradeWindow(SelectedGrade.Subject, SelectedGrade.Grade, SelectedGrade.Date);
                if (window.ShowDialog() == true)
                {
                    SelectedGrade.Subject = window.Subject;
                    SelectedGrade.Grade = window.Grade;
                    SelectedGrade.Date = window.Date;
                    GradesGrid.Items.Refresh();
                }
            }
        }

        private void DeleteGrade()
        {
            if (SelectedGrade != null)
            {
                if (MessageBox.Show($"Удалить оценку {SelectedGrade.Subject}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Grades.Remove(SelectedGrade);
                }
            }
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }

        private void ShowSchedule()
        {
            MessageBox.Show("Расписание загружено", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    public class ScheduleItem
    {
        public int LessonNumber { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
    }

    public class GradeItem
    {
        public string Subject { get; set; }
        public int Grade { get; set; }
        public string Date { get; set; }
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

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}