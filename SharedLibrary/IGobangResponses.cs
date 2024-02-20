using SharedLibrary.ResponseArgs.Gobang;

namespace SharedLibrary;

public interface IGobangResponses
{
    Task CreateRoomEvent(CreateRoomEventArgs e);
    Task RoomHasExistedEvent(RoomHasExistedEventArgs e);

    Task GetIntoRoomEvent(GetIntoRoomEventArgs e);
    Task RoomNotExistEvent(RoomNotExistEventArgs e);
    Task FailToGetIntoRoomEvent(FailToGetIntoRoomEventArgs e);

    Task StartGameEvent(StartGameEventArgs e);

    Task MoveChessEvent(MoveChessEventArgs e);
    Task FailToMoveChessEvent(FailToMoveChessEventArgs e);
    Task PassMoveChessEvent(PassMoveChessEventArgs e);

    Task PlayerWinGameEvent(PlayerWinGameEventArgs e);
}

