namespace SharedLibrary.ResponseArgs.Gobang;

public class PlayerJoinGameFailedEventArgs : EventArgs
{
    public required string GameId { get; set; }
    public required string PlayerId { get; set; }
}

