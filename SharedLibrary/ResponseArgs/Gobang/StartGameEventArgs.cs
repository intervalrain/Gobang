namespace SharedLibrary.ResponseArgs.Gobang;

public class StartGameEventArgs : EventArgs
{
    public required string GameId { get; set; }
    public required string BlackId { get; set; }
    public required string WhiteId { get; set; }
}

