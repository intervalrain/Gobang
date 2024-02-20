namespace SharedLibrary.ResponseArgs.Gobang;

public class PlayerWinGameEventArgs : EventArgs
{
	public required string PlayerId { get; set; }
}

