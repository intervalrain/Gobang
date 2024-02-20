using Domain.Events;
using Microsoft.AspNetCore.SignalR;
using Server.Common;
using SharedLibrary;
using SharedLibrary.ResponseArgs.Gobang;

namespace Server.Hubs.EventHandlers;

public class GetIntoRoomEventHandler : GobangEventHandlerBase<GetIntoRoomEvent>
{
    private readonly IHubContext<GobangHub, IGobangResponses> _hubContext;

    public GetIntoRoomEventHandler(IHubContext<GobangHub, IGobangResponses> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override Task HandleSpecificEvent(GetIntoRoomEvent e)
    {
        return _hubContext.Clients.All.GetIntoRoomEvent(new GetIntoRoomEventArgs
        {
            GameId = e.GameId,
            PlayerId = e.PlayerId,
            Password = e.Password
        });
    }
}

public class RoomNotExistEventHandler : GobangEventHandlerBase<RoomNotExistEvent>
{
    private readonly IHubContext<GobangHub, IGobangResponses> _hubContext;

    public RoomNotExistEventHandler(IHubContext<GobangHub, IGobangResponses> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override Task HandleSpecificEvent(RoomNotExistEvent e)
    {
        return _hubContext.Clients.All.RoomNotExistEvent(new RoomNotExistEventArgs
        {
            GameId = e.GameId,
            PlayerId = e.PlayerId,
            Password = e.Password
        });
    }
}

public class FailToGetIntoRoomEventHandler : GobangEventHandlerBase<FailToGetIntoRoomEvent>
{
    private readonly IHubContext<GobangHub, IGobangResponses> _hubContext;

    public FailToGetIntoRoomEventHandler(IHubContext<GobangHub, IGobangResponses> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override Task HandleSpecificEvent(FailToGetIntoRoomEvent e)
    {
        return _hubContext.Clients.All.FailToGetIntoRoomEvent(new FailToGetIntoRoomEventArgs
        {
            GameId = e.GameId,
            PlayerId = e.PlayerId,
            Password = e.Password
        });
    }
}

