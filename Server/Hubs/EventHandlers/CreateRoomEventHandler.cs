using Domain.Events;
using Microsoft.AspNetCore.SignalR;
using Server.Common;
using SharedLibrary;
using SharedLibrary.ResponseArgs.Gobang;

namespace Server.Hubs.EventHandlers;

public class CreateRoomEventHandler : GobangEventHandlerBase<CreateRoomEvent>
{
    private readonly IHubContext<GobangHub, IGobangResponses> _hubContext;

    public CreateRoomEventHandler(IHubContext<GobangHub, IGobangResponses> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override Task HandleSpecificEvent(CreateRoomEvent e)
    {
        return _hubContext.Clients.All.CreateRoomEvent(new CreateRoomEventArgs
        {
            GameId = e.GameId,
            PlayerId = e.PlayerId,
            Password = e.Password 
        });
    }
}

public class RoomHasExistedEventHandler : GobangEventHandlerBase<RoomHasExistedEvent>
{
    private readonly IHubContext<GobangHub, IGobangResponses> _hubContext;

    public RoomHasExistedEventHandler(IHubContext<GobangHub, IGobangResponses> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override Task HandleSpecificEvent(RoomHasExistedEvent e)
    {
        return _hubContext.Clients.All.RoomHasExistedEvent(new RoomHasExistedEventArgs
        {
            GameId = e.GameId,
            PlayerId = e.PlayerId
        });
    }
}

