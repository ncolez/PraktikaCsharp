using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<GradeItem> gradeList;

        public MainWindow()
        {
            InitializeComponent();
            LoadSchedule();
            LoadGrades();
            LoadSubjects();
        }

        private void LoadSchedule()
        {
            var scheduleList = new List<ScheduleItem>();

            scheduleList.Add(new ScheduleItem { LessonNumber = 1, Monday = "Физика", Tuesday = "Математика", Wednesday = "Русский язык", Thursday = "Английский", Friday = "История" });
     
            scheduleList.Add(new ScheduleItem { LessonNumber = 2, Monday = "Математика", Tuesday = "Физика", Wednesday = "Английский", Thursday = "Математика", Friday = "Литература" });
          
            scheduleList.Add(new ScheduleItem { LessonNumber = 3, Monday = "Русский язык", Tuesday = "Химия", Wednesday = "Математика", Thursday = "Физкультура", Friday = "Математика" });
       
            scheduleList.Add(new ScheduleItem { LessonNumber = 4, Monday = "Английский", Tuesday = "Биология", Wednesday = "Физика", Thursday = "Русский язык", Friday = "ОБЖ" });

            scheduleList.Add(new ScheduleItem { LessonNumber = 5, Monday = "История", Tuesday = "Русский язык", Wednesday = "Литература", Thursday = "Химия", Friday = "Физика" });
    
            scheduleList.Add(new ScheduleItem { LessonNumber = 6, Monday = "", Tuesday = "", Wednesday = "Физкультура", Thursday = "", Friday = "" });

            ScheduleGrid.ItemsSource = scheduleList;
        }

        private void LoadGrades()
        {
            gradeList = new ObservableCollection<GradeItem>();
            gradeList.Add(new GradeItem { Subject = "Математика", Grade = 5, Date = "01.03.2025" });
            gradeList.Add(new GradeItem { Subject = "Математика", Grade = 7, Date = "05.03.2025" });
            gradeList.Add(new GradeItem { Subject = "Физика", Grade = 6, Date = "02.03.2025" });
            gradeList.Add(new GradeItem { Subject = "Программирование", Grade = 5, Date = "03.03.2025" });
            gradeList.Add(new GradeItem { Subject = "Английский", Grade = 9, Date = "04.03.2025" });
            gradeList.Add(new GradeItem { Subject = "Русский язык", Grade = 8, Date = "06.03.2025" });

            GradesGrid.ItemsSource = gradeList;
        }

        private void LoadSubjects()
        {
            SubjectCombo.Items.Add("Математика");
            SubjectCombo.Items.Add("Физика");
            SubjectCombo.Items.Add("Программирование");
            SubjectCombo.Items.Add("Английский");
            SubjectCombo.Items.Add("История");
            SubjectCombo.Items.Add("Русский язык");
            SubjectCombo.Items.Add("Литература");
            SubjectCombo.Items.Add("Химия");
            SubjectCombo.Items.Add("Биология");
            SubjectCombo.Items.Add("Физкультура");
            SubjectCombo.SelectedIndex = 0;
        }

        private void AddGradeButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectCombo.SelectedItem == null || GradeCombo.SelectedItem == null)
            {
                MessageBox.Show("Выберите предмет и оценку!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string subject = SubjectCombo.SelectedItem.ToString();
            ComboBoxItem selectedGrade = GradeCombo.SelectedItem as ComboBoxItem;
            int grade = Convert.ToInt32(selectedGrade.Content);
            string date = DateTime.Now.ToString("dd.MM.yyyy");

            if (grade < 1 || grade > 10)
            {
                MessageBox.Show("Оценка должна быть от 1 до 10!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            gradeList.Add(new GradeItem { Subject = subject, Grade = grade, Date = date });

            MessageBox.Show("Оценка добавлена!", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);
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
}