using System;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace z1.Services
{
    public class ChatServerService
    {
        private NamedPipeServerStream? _server;
        private bool _isRunning;
        private static List<string> _messages = new List<string>();

        public void StartServer()
        {
            _isRunning = true;
            Task.Run(RunServerAsync);
        }

        private async Task RunServerAsync()
        {
            while (_isRunning)
            {
                try
                {
                    _server = new NamedPipeServerStream("StudentChat", PipeDirection.InOut);
                    _server.WaitForConnection();

                    byte[] buffer = new byte[4096];
                    int bytesRead = await _server.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        _messages.Add(message);
                    }

                    _server.Disconnect();
                    _server.Dispose();
                    _server = null;
                }
                catch
                {
                    await Task.Delay(100);
                }
            }
        }

        public static List<string> GetMessages()
        {
            return _messages;
        }

        public void StopServer()
        {
            _isRunning = false;
            _server?.Dispose();
        }
    }
}