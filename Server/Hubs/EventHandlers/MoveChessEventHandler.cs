using Domain.Events;
using Microsoft.AspNetCore.SignalR;
using Server.Common;
using SharedLibrary;
using SharedLibrary.ResponseArgs.Gobang;

namespace Server.Hubs.EventHandlers;

public class MoveChessEventHandler : GobangEventHandlerBase<MoveChessEvent>
{
    private readonly IHubContext<GobangHub, IGobangResponses> _hubContext;

    public MoveChessEventHandler(IHubContext<GobangHub, IGobangResponses> hubContext)
	{
        _hubContext = hubContext;
    }

    protected override Task HandleSpecificEvent(MoveChessEvent e)
    {
        return _hubContext.Clients.All.MoveChessEvent(new MoveChessEventArgs
        {
            GameId = e.GameId,
            PlayerId = e.PlayerId,
            Row = e.Row,
            Col = e.Col 
        });
    }
}

public class FailToMoveChessEventHandler : GobangEventHandlerBase<FailToMoveChessEvent>
{
    private readonly IHubContext<GobangHub, IGobangResponses> _hubContext;

    public FailToMoveChessEventHandler(IHubContext<GobangHub, IGobangResponses> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override Task HandleSpecificEvent(FailToMoveChessEvent e)
    {
        return _hubContext.Clients.All.FailToMoveChessEvent(new FailToMoveChessEventArgs
        {
            GameId = e.GameId,
            PlayerId = e.PlayerId,
            Row = e.Row,
            Col = e.Col
        });
    }
}

public class PassMoveChessEventHandler : GobangEventHandlerBase<PassMoveChessEvent>
{
    private readonly IHubContext<GobangHub, IGobangResponses> _hubContext;

    public PassMoveChessEventHandler(IHubContext<GobangHub, IGobangResponses> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override Task HandleSpecificEvent(PassMoveChessEvent e)
    {
        return _hubContext.Clients.All.PassMoveChessEvent(new PassMoveChessEventArgs
        {
            GameId = e.GameId,
            PlayerId = e.PlayerId,
        });
    }
}