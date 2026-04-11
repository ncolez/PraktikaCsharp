using System.Linq;
using System.Windows;
using z1.Data;
using z1.Services;

namespace z1
{
    public partial class LoginWindow : Window
    {
        private readonly AppDbContext _context;

        public LoginWindow()
        {
            InitializeComponent();
            _context = new AppDbContext();
            _context.Database.EnsureCreated();
            EnsureDefaultUsers();
            EnsureDefaultStudents();
        }

        private void EnsureDefaultUsers()
        {
            if (!_context.Users.Any())
            {
                var dataService = new DataService();
                _context.Users.Add(new UserModel
                {
                    Id = 1,
                    Username = "teacher",
                    PasswordHash = dataService.GetHash("123"),
                    Role = "Teacher",
                    FullName = "Иван Петрович"
                });
                _context.Users.Add(new UserModel
                {
                    Id = 2,
                    Username = "student",
                    PasswordHash = dataService.GetHash("123"),
                    Role = "Student",
                    FullName = "Иван Иванов"
                });
                _context.SaveChanges();
            }
        }

        private void EnsureDefaultStudents()
        {
            if (!_context.Students.Any())
            {
                _context.Students.Add(new StudentModel { Id = 1, FullName = "Иван Иванов", UserId = 2 });
                _context.Students.Add(new StudentModel { Id = 2, FullName = "Петр Петров", UserId = 0 });
                _context.Students.Add(new StudentModel { Id = 3, FullName = "Анна Сидорова", UserId = 0 });
                _context.SaveChanges();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameBox.Text;
            var password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorText.Text = "Введите логин и пароль";
                return;
            }

            var dataService = new DataService();
            var passwordHash = dataService.GetHash(password);

            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == passwordHash);

            if (user == null)
            {
                ErrorText.Text = "Неверный логин или пароль";
                return;
            }

            var mainWindow = new MainWindow(user);
            mainWindow.Show();
            Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameBox.Text;
            var password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorText.Text = "Введите логин и пароль";
                return;
            }

            if (_context.Users.Any(u => u.Username == username))
            {
                ErrorText.Text = "Пользователь с таким логином уже существует";
                return;
            }

            var dataService = new DataService();
            var newUser = new UserModel
            {
                Username = username,
                PasswordHash = dataService.GetHash(password),
                Role = "Student",
                FullName = username
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            var newStudent = new StudentModel
            {
                FullName = username,
                UserId = newUser.Id
            };
            _context.Students.Add(newStudent);
            _context.SaveChanges();

            MessageBox.Show("Регистрация успешна! Теперь войдите в систему.", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);

            UsernameBox.Text = "";
            PasswordBox.Password = "";
            ErrorText.Text = "";
        }
    }
}