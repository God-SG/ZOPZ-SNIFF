using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SNIFF.Classes.Auth.Models;
using SocketIOClient;

namespace SNIFF.Classes.Auth;

public class ChatClient
{
	private SocketIO _socket;

	public event Action OnConnected;

	public event Action OnDisconnected;

	public event Action<ChatMessage> OnMessageReceived;

	public event Action<IEnumerable<ChatMessage>> OnMessagesReceived;

	public ChatClient(string authToken)
	{
	}

	private void _socket_OnPong(object sender, TimeSpan e)
	{
		Console.WriteLine(e.ToString());
	}

	private void _socket_OnPing(object sender, EventArgs e)
	{
		Console.WriteLine(e);
	}

	private void _socket_OnError(object sender, string e)
	{
		Console.WriteLine(e);
	}

	public async Task ConnectAsync()
	{
		try
		{
			Console.WriteLine("Attempting to connect...");
			await _socket.ConnectAsync();
			Console.WriteLine("Connected successfully.");
		}
		catch (Exception ex)
		{
			Console.WriteLine("Connection failed: " + ex.Message);
		}
	}

	private void Connected()
	{
		this.OnConnected?.Invoke();
	}

	public async Task JoinRoomAsync(string room)
	{
		await _socket.EmitAsync("joinRoom", room);
	}

	private void Disconnected()
	{
		this.OnDisconnected?.Invoke();
	}

	private void OnMessage(ChatMessage msg)
	{
		this.OnMessageReceived?.Invoke(msg);
	}

	private void OnMessages(IEnumerable<ChatMessage> msgs)
	{
		Console.WriteLine("Messages Received:");
		this.OnMessagesReceived?.Invoke(msgs);
	}

	public async Task DisconnectAsync()
	{
		await _socket.DisconnectAsync();
	}

	public async Task SendMessageAsync(string message)
	{
		await _socket.EmitAsync("sendMessage", message);
	}
}
