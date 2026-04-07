using System.Windows;
using System.Windows.Controls;

namespace z1
{
    public partial class GradeWindow : Window
    {
        public string StudentName { get; set; }
        public string Subject { get; set; }
        public int Grade { get; set; }

        public GradeWindow()
        {
            InitializeComponent();
            SubjectCombo.SelectedIndex = 0;
            GradeCombo.SelectedIndex = 3;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StudentNameBox.Text))
            {
                MessageBox.Show(
                    "Введите имя студента!",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            var selectedSubject = SubjectCombo.SelectedItem as ComboBoxItem;
            var selectedGrade = GradeCombo.SelectedItem as ComboBoxItem;

            StudentName = StudentNameBox.Text;
            Subject = selectedSubject?.Content.ToString();
            Grade = int.Parse(selectedGrade?.Content.ToString());

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