using SocketIOClient;
using ZOPZ_SNIFF.Json.Auth;

namespace ZOPZ_SNIFF.Auth
{
    public class ChatClient
    {
        private bool _disposed = false;
        public event Action? OnDisconnected;
        public event Action<ChatMessage>? OnMessageReceived;
        public event Action<IEnumerable<ChatMessage>>? OnMessagesReceived;
        private SocketIOClient.SocketIO _socket { get; set; }

        public ChatClient(string authToken)
        {
            _socket = new SocketIOClient.SocketIO("https://zopzsniff.xyz/chat", new SocketIOOptions
            {
                Path = "/chat/ws",
                UseTrailingSlash = false,
                ExtraHeaders = new Dictionary<string, string>
                {
                    { "Authorization", authToken },
                    { "User-Agent", "zopz-chatter/1.0.0" }
                }
            });
            _socket.OnDisconnected += delegate (object? sender, string e)
            {
                Disconnected();
            };
            _socket.OnError += _socket_OnError;
            _socket.OnPing += _socket_OnPing;
            _socket.OnPong += _socket_OnPong;
            _socket.On("message", delegate (SocketIOResponse msg)
            {
                OnMessage(msg.GetValue<ChatMessage>(0));
            });
            _socket.On("messages", delegate (SocketIOResponse msgs)
            {
                OnMessages(msgs.GetValue<List<ChatMessage>>(0));
            });
        }

        private void Disconnected()
        {
            if (!_disposed)
                OnDisconnected?.Invoke();
        }

        private void OnMessage(ChatMessage msg)
        {

            if (!_disposed)
                OnMessageReceived?.Invoke(msg);
        }
        private void OnMessages(IEnumerable<ChatMessage> msgs)
        {
            if (!_disposed)
                OnMessagesReceived?.Invoke(msgs);
        }

        private void _socket_OnPong(object? sender, TimeSpan e)
        {
            // MessageBox.Show($"Pong: {e.ToString()}");
        }

        private void _socket_OnPing(object? sender, EventArgs e)
        {
            //MessageBox.Show("Ping received");
        }

        private void _socket_OnError(object? sender, string e)
        {
            //MessageBox.Show($"Error: {e}");
        }

        public async Task Connect()
        {
            await _socket.ConnectAsync();
        }

        public async Task JoinRoom(string room)
        {
            if (!_disposed)
                await _socket.EmitAsync("joinRoom", [room]);
        }

        public async Task Disconnect()
        {
            try
            {
                if (!_disposed)
                {
                    await _socket.DisconnectAsync();
                    _disposed = true;
                }
            }
            catch { }
        }

        public async void SendMessage(string message)
        {
            if (!_disposed)
                await _socket.EmitAsync("sendMessage", [message]);
        }
    }
}
