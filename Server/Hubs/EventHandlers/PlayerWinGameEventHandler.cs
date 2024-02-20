using Domain.Events;
using Microsoft.AspNetCore.SignalR;
using Server.Common;
using SharedLibrary;
using SharedLibrary.ResponseArgs.Gobang;

namespace Server.Hubs.EventHandlers;

public class PlayerWinGameEventHandler : GobangEventHandlerBase<PlayerWinGameEvent>
{
    private readonly IHubContext<GobangHub, IGobangResponses> _hubContext;

    public PlayerWinGameEventHandler(IHubContext<GobangHub, IGobangResponses> hubContext)
	{
        _hubContext = hubContext;
    }

    protected override Task HandleSpecificEvent(PlayerWinGameEvent e)
    {
        return _hubContext.Clients.All.PlayerWinGameEvent(new PlayerWinGameEventArgs
        {
            PlayerId = e.PlayerId
        });
    }
}

