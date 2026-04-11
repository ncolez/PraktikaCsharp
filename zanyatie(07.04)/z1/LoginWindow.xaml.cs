using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace z1
{
    public partial class LoginWindow : Window
    {
        private readonly DataService _dataService = new DataService();
        private ObservableCollection<UserModel> _users = new ObservableCollection<UserModel>();

        public LoginWindow()
        {
            InitializeComponent();
            _ = LoadUsers();
        }

        private async Task LoadUsers()
        {
            _users = await _dataService.LoadUsersAsync();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameBox.Text;
            var password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorText.Text = "Введите логин и пароль";
                return;
            }

            var passwordHash = _dataService.GetHash(password);
            var user = _users.FirstOrDefault(u => u.Username == username && u.PasswordHash == passwordHash);

            if (user == null)
            {
                ErrorText.Text = "Неверный логин или пароль";
                return;
            }

            var mainWindow = new MainWindow(user);
            mainWindow.Show();
            Close();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameBox.Text;
            var password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorText.Text = "Введите логин и пароль";
                return;
            }

            if (_users.Any(u => u.Username == username))
            {
                ErrorText.Text = "Пользователь с таким логином уже существует";
                return;
            }

            var newUser = new UserModel
            {
                Id = _users.Count + 1,
                Username = username,
                PasswordHash = _dataService.GetHash(password),
                Role = "Student",
                FullName = username
            };

            _users.Add(newUser);
            await _dataService.SaveUsersAsync(_users);

            MessageBox.Show("Регистрация успешна! Теперь войдите в систему.", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);

            UsernameBox.Text = string.Empty;
            PasswordBox.Password = string.Empty;
            ErrorText.Text = string.Empty;
        }
    }
}