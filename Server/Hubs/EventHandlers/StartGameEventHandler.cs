using Domain.Events;
using Microsoft.AspNetCore.SignalR;
using Server.Common;
using SharedLibrary;
using SharedLibrary.ResponseArgs.Gobang;

namespace Server.Hubs.EventHandlers;

public class StartGameEventHandler : GobangEventHandlerBase<StartGameEvent>
{
    private readonly IHubContext<GobangHub, IGobangResponses> _hubContext;

    public StartGameEventHandler(IHubContext<GobangHub, IGobangResponses> hubContext)
	{
        _hubContext = hubContext;
    }

    protected override Task HandleSpecificEvent(StartGameEvent e)
    {
        return _hubContext.Clients.All.StartGameEvent(new StartGameEventArgs
        {
            GameId = e.GameId,
            BlackId = e.BlackId,
            WhiteId = e.WhiteId
        });
    }
}

