using System.Windows;
using z1.Services;

namespace z1
{
    public partial class App : Application
    {
        private ChatServerService? _chatServer;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _chatServer = new ChatServerService();
            _chatServer.StartServer();

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _chatServer?.StopServer();
            base.OnExit(e);
        }
    }
}