namespace SharedLibrary.ResponseArgs.Gobang;

public class CreateRoomEventArgs : EventArgs
{
    public required string GameId { get; set; }
    public required string PlayerId { get; set; }
    public required string Password { get; set; }
}

public class RoomHasExistedEventArgs : EventArgs
{
    public required string GameId { get; set; }
    public required string PlayerId { get; set; }
}