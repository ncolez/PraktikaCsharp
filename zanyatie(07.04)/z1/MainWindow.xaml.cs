using System.Windows;
using z1.Services;

namespace z1
{
    public partial class MainWindow : Window
    {
        private readonly NotificationService _notificationService = new NotificationService();

        public MainWindow(UserModel currentUser)
        {
            InitializeComponent();
            var viewModel = new StudentViewModel(currentUser);
            DataContext = viewModel;
            Loaded += async (s, e) =>
            {
                await viewModel.LoadDataAsync();
                await CheckNotifications();
            };
        }

        private async System.Threading.Tasks.Task CheckNotifications()
        {
            var notification = await _notificationService.ReadNotificationAsync();
            if (!string.IsNullOrEmpty(notification))
            {
                _notificationService.ShowNotification(notification);
            }
        }
    }
}