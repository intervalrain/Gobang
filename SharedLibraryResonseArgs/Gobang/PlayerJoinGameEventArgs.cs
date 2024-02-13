namespace SharedLibrary.ResponseArgs.Gobang;

public class MoveChessEventArgs : EventArgs
{
	public required string PlayerId { get; set; }
    public required int Row { get; set; }
    public required int Col { get; set; }
}

