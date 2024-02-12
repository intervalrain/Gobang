using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs;

public class BlazorChatSampleHub : Hub
{
	public const string HubUrl = "/chat";

	public async Task BroadCast(string userName, string message)
	{
		await Clients.All.SendAsync("Broadcast", userName, message);
	}

	public override Task OnConnectedAsync()
	{
		Console.WriteLine($"{Context.ConnectionId} connected");
		return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? e)
    {
		Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
        await base.OnDisconnectedAsync(e);
    }
}

