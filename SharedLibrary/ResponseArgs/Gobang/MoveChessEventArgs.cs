namespace SharedLibrary.ResponseArgs.Gobang;

public class MoveChessEventArgs : EventArgs
{
	public required string GameId;
    public required string PlayerId;
    public required int Row;
    public required int Col;
}

public class FailToMoveChessEventArgs : EventArgs
{
    public required string GameId;
    public required string PlayerId;
    public required int Row;
    public required int Col;
}

public class PassMoveChessEventArgs : EventArgs
{
    public required string GameId;
    public required string PlayerId;
}