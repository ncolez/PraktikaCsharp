using System;
using System.Windows;
using System.Windows.Controls;

namespace z1
{
    public partial class GradeWindow : Window
    {
        public string Subject { get; set; }
        public int Grade { get; set; }
        public string Date { get; set; }

        public GradeWindow()
        {
            InitializeComponent();
            DatePicker.SelectedDate = DateTime.Now;
        }

        public GradeWindow(string subject, int grade, string date)
        {
            InitializeComponent();
            Title = "Редактирование оценки";

            SetComboBoxValue(SubjectCombo, subject);
            SetComboBoxValue(GradeCombo, grade.ToString());
            DatePicker.SelectedDate = DateTime.ParseExact(date, "dd.MM.yyyy", null);
        }

        private void SetComboBoxValue(ComboBox combo, string value)
        {
            foreach (ComboBoxItem item in combo.Items)
            {
                if (item.Content.ToString() == value)
                {
                    combo.SelectedItem = item;
                    break;
                }
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Subject = (SubjectCombo.SelectedItem as ComboBoxItem)?.Content.ToString();
            Grade = int.Parse((GradeCombo.SelectedItem as ComboBoxItem).Content.ToString());
            Date = DatePicker.SelectedDate?.ToString("dd.MM.yyyy");
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}