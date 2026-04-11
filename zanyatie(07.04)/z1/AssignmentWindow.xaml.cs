using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using z1.Models;
using z1.Services;

namespace z1
{
    public partial class AssignmentWindow : Window
    {
        private readonly AssignmentService _assignmentService;
        private ObservableCollection<AssignmentModel> _allAssignments;
        private readonly UserModel _currentUser;

        public AssignmentWindow(UserModel currentUser)
        {
            InitializeComponent();
            _assignmentService = new AssignmentService();
            _allAssignments = new ObservableCollection<AssignmentModel>();
            _currentUser = currentUser;

            DataContext = this;

            Loaded += OnLoaded;
            SubjectCombo.SelectionChanged += SubjectCombo_SelectionChanged;
        }

        public bool IsTeacher
        {
            get { return _currentUser.Role == "Teacher"; }
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            LoadingBorder.Visibility = Visibility.Visible;
            _allAssignments = await _assignmentService.LoadAssignmentsAsync();
            LoadingBorder.Visibility = Visibility.Collapsed;
            RefreshAssignmentsList();
        }

        private void RefreshAssignmentsList()
        {
            if (SubjectCombo.SelectedItem == null)
            {
                AssignmentsItemsControl.ItemsSource = null;
                EmptyTextBlock.Visibility = Visibility.Visible;
                return;
            }

            var selectedItem = SubjectCombo.SelectedItem as ComboBoxItem;
            if (selectedItem == null)
            {
                AssignmentsItemsControl.ItemsSource = null;
                EmptyTextBlock.Visibility = Visibility.Visible;
                return;
            }

            string selectedSubject = selectedItem.Content?.ToString() ?? string.Empty;

            if (selectedSubject == "Выберите предмет" || string.IsNullOrEmpty(selectedSubject))
            {
                AssignmentsItemsControl.ItemsSource = null;
                EmptyTextBlock.Visibility = Visibility.Visible;
                return;
            }

            EmptyTextBlock.Visibility = Visibility.Collapsed;

            var filtered = new ObservableCollection<AssignmentModel>();
            foreach (var assignment in _allAssignments)
            {
                if (assignment.Subject == selectedSubject)
                {
                    filtered.Add(assignment);
                }
            }

            AssignmentsItemsControl.ItemsSource = filtered;
        }

        private void SubjectCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshAssignmentsList();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser.Role != "Teacher")
            {
                return;
            }

            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var assignment = button.Tag as AssignmentModel;
            if (assignment == null)
            {
                return;
            }

            var contextMenu = new ContextMenu();

            var editItem = new MenuItem { Header = "Изменить" };
            editItem.Click += (s, args) => EditAssignment(assignment);

            var deleteItem = new MenuItem { Header = "Удалить" };
            deleteItem.Click += (s, args) => DeleteAssignment(assignment);

            contextMenu.Items.Add(editItem);
            contextMenu.Items.Add(deleteItem);

            contextMenu.IsOpen = true;
        }

        private async void EditAssignment(AssignmentModel assignment)
        {
            var subjects = new System.Collections.Generic.List<string>
            {
                "Математика",
                "Физика",
                "Программирование",
                "Английский язык",
                "Русский язык",
                "Литература",
                "Химия",
                "Биология",
                "История"
            };

            var window = new AssignmentEditWindow(assignment, subjects);

            if (window.ShowDialog() == true)
            {
                assignment.Title = window.AssignmentTitle;
                assignment.Description = window.AssignmentDescription;
                assignment.DueDate = window.AssignmentDueDate;

                await _assignmentService.SaveAssignmentsAsync(_allAssignments);
                RefreshAssignmentsList();
            }
        }

        private async void DeleteAssignment(AssignmentModel assignment)
        {
            var result = MessageBox.Show(
                $"Удалить задание \"{assignment.Title}\"?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _allAssignments.Remove(assignment);
                await _assignmentService.SaveAssignmentsAsync(_allAssignments);
                RefreshAssignmentsList();
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var subjects = new System.Collections.Generic.List<string>
            {
                "Математика",
                "Физика",
                "Программирование",
                "Английский язык",
                "Русский язык",
                "Литература",
                "Химия",
                "Биология",
                "История"
            };

            var window = new AssignmentEditWindow(null, subjects);

            if (window.ShowDialog() == true)
            {
                var newId = 1;
                if (_allAssignments.Count > 0)
                {
                    newId = _allAssignments[_allAssignments.Count - 1].Id + 1;
                }

                var newAssignment = new AssignmentModel
                {
                    Id = newId,
                    Subject = window.AssignmentSubject,
                    Title = window.AssignmentTitle,
                    Description = window.AssignmentDescription,
                    DueDate = window.AssignmentDueDate,
                    CreatedByUserId = 1
                };

                _allAssignments.Add(newAssignment);
                await _assignmentService.SaveAssignmentsAsync(_allAssignments);
                RefreshAssignmentsList();
            }
        }
    }
}