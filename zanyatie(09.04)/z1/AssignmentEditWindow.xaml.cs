using System;
using System.Collections.Generic;
using System.Windows;
using z1.Models;

namespace z1
{
    public partial class AssignmentEditWindow : Window
    {
        public string AssignmentSubject { get; set; } = string.Empty;
        public string AssignmentTitle { get; set; } = string.Empty;
        public string AssignmentDescription { get; set; } = string.Empty;
        public DateTime AssignmentDueDate { get; set; }

        public AssignmentEditWindow(AssignmentModel? assignment, List<string> subjects)
        {
            InitializeComponent();

            if (subjects != null)
            {
                SubjectCombo.ItemsSource = subjects;
                SubjectCombo.SelectedIndex = 0;
            }

            if (assignment != null)
            {
                TitleTextBlock.Text = "✏ Редактирование задания";
                SubjectCombo.SelectedItem = assignment.Subject;
                TitleBox.Text = assignment.Title;
                DescriptionBox.Text = assignment.Description;
                DueDatePicker.SelectedDate = assignment.DueDate;
            }
            else
            {
                TitleTextBlock.Text = "➕ Новое задание";
                DueDatePicker.SelectedDate = DateTime.Now.AddDays(7);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectCombo.SelectedItem == null)
            {
                MessageBox.Show("Выберите предмет!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TitleBox.Text))
            {
                MessageBox.Show("Введите название задания!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AssignmentSubject = SubjectCombo.SelectedItem.ToString() ?? string.Empty;
            AssignmentTitle = TitleBox.Text;
            AssignmentDescription = DescriptionBox.Text;
            AssignmentDueDate = DueDatePicker.SelectedDate ?? DateTime.Now;

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