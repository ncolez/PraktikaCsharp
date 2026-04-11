using System;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using z1.Services;

namespace z1
{
    public partial class ChatWindow : Window
    {
        private readonly string _userName;
        private NamedPipeClientStream? _client;
        private System.Timers.Timer? _timer;
        private int _lastMessageCount;

        public ChatWindow(string userName)
        {
            InitializeComponent();
            _userName = userName;
            Title = $"Чат - {_userName}";
            Loaded += OnLoaded;
            Closed += OnClosed;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await ConnectToServerAsync();
            StartTimer();
        }

        private async Task ConnectToServerAsync()
        {
            try
            {
                _client = new NamedPipeClientStream(".", "StudentChat", PipeDirection.InOut);
                await _client.ConnectAsync(3000);
                MessagesList.Items.Add("Подключено к чату");
            }
            catch (Exception ex)
            {
                MessagesList.Items.Add($"Ошибка: {ex.Message}");
            }
        }

        private void StartTimer()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += CheckNewMessages;
            _timer.Start();
        }

        private void CheckNewMessages(object? sender, System.Timers.ElapsedEventArgs e)
        {
            var messages = ChatServerService.GetMessages();

            if (messages.Count > _lastMessageCount)
            {
                for (int i = _lastMessageCount; i < messages.Count; i++)
                {
                    string msg = messages[i];
                    Dispatcher.Invoke(() =>
                    {
                        MessagesList.Items.Add(msg);
                        MessagesList.ScrollIntoView(MessagesList.Items[MessagesList.Items.Count - 1]);
                    });
                }
                _lastMessageCount = messages.Count;
            }
        }

        private async void SendMessage(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            if (_client == null || !_client.IsConnected)
            {
                MessagesList.Items.Add("Нет подключения");
                return;
            }

            string message = $"{_userName}: {text}";

            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                await _client.WriteAsync(buffer, 0, buffer.Length);
                await _client.FlushAsync();
                MessageBox.Text = "";
            }
            catch (Exception ex)
            {
                MessagesList.Items.Add($"Ошибка: {ex.Message}");
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(MessageBox.Text);
        }

        private void MessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage(MessageBox.Text);
            }
        }

        private void OnClosed(object? sender, EventArgs e)
        {
            _timer?.Stop();
            _timer?.Dispose();
            _client?.Close();
            _client?.Dispose();
        }
    }
}