using System;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace z1.Services
{
    public class NotificationService
    {
        private const string MemoryMapName = "StudentNotifications";
        private const int MemoryMapSize = 1024;

        public void SendNotification(string message, string targetUser)
        {
            try
            {
                using var mmf = MemoryMappedFile.CreateOrOpen(MemoryMapName, MemoryMapSize);
                using var accessor = mmf.CreateViewAccessor();

                string fullMessage = $"{targetUser}|{message}|{DateTime.Now:HH:mm:ss}";
                byte[] bytes = Encoding.UTF8.GetBytes(fullMessage);
                accessor.WriteArray(0, bytes, 0, bytes.Length < MemoryMapSize ? bytes.Length : MemoryMapSize);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка отправки уведомления: {ex.Message}");
            }
        }

        public async Task<string?> ReadNotificationAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    MemoryMappedFile? mmf = null;

                    try
                    {
                        mmf = MemoryMappedFile.OpenExisting(MemoryMapName);
                    }
                    catch
                    {
                        return null;
                    }

                    if (mmf == null)
                    {
                        return null;
                    }

                    using (mmf)
                    using (var accessor = mmf.CreateViewAccessor())
                    {
                        byte[] bytes = new byte[MemoryMapSize];
                        accessor.ReadArray(0, bytes, 0, MemoryMapSize);

                        string fullMessage = Encoding.UTF8.GetString(bytes).TrimEnd('\0');

                        if (string.IsNullOrWhiteSpace(fullMessage))
                        {
                            return null;
                        }

                        return fullMessage;
                    }
                }
                catch
                {
                    return null;
                }
            });
        }

        public void ShowNotification(string fullMessage)
        {
            var parts = fullMessage.Split('|');
            if (parts.Length >= 2)
            {
                string targetUser = parts[0];
                string message = parts[1];
                string time = parts.Length > 2 ? parts[2] : string.Empty;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show(
                        $"{message}\nВремя: {time}",
                        "Новое уведомление",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                });
            }
        }
    }
}