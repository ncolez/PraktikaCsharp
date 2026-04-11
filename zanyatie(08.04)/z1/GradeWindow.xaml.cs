using System.Windows;
using System.Windows.Controls;

namespace z1
{
    public partial class GradeWindow : Window
    {
        public string Subject { get; set; } = string.Empty;
        public int Grade { get; set; }

        public GradeWindow(string studentName)
        {
            InitializeComponent();
            StudentNameText.Text = studentName;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedSubject = SubjectCombo.SelectedItem as ComboBoxItem;
            var selectedGrade = GradeCombo.SelectedItem as ComboBoxItem;

            if (selectedSubject == null || selectedGrade == null)
            {
                return;
            }

            Subject = selectedSubject.Content.ToString() ?? string.Empty;
            Grade = int.Parse(selectedGrade.Content.ToString() ?? "0");

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