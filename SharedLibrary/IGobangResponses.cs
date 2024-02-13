using SharedLibrary.ResponseArgs.Gobang;

namespace SharedLibrary;

public interface IGobangResponses
{
	Task MoveChessEvent(MoveChessEventArgs e);
    Task PlayerJoinGameFailedEvent(PlayerJoinGameFailedEventArgs e);
    Task PlayerJoinGameEvent(PlayerJoinGameEventArgs e);
    Task WelcomeEvent(WelcomeEventArgs e);
}

