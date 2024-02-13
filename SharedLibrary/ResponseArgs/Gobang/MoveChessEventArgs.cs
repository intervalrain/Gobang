namespace SharedLibrary.ResponseArgs.Gobang;

public class MoveChessEventArgs : EventArgs
{
	public required string GameId;
    public required string PlayerId;
}

