namespace SharedLibrary.ResponseArgs.Gobang;

public class GetIntoRoomEventArgs : EventArgs
{
    public required string GameId { get; set; }
    public required string PlayerId { get; set; }
    public required string? Password { get; set; }
}

public class RoomNotExistEventArgs : EventArgs
{
    public required string GameId { get; set; }
    public required string PlayerId { get; set; }
    public required string? Password { get; set; }
}

public class FailToGetIntoRoomEventArgs : EventArgs
{
    public required string GameId { get; set; }
    public required string PlayerId { get; set; }
    public required string? Password { get; set; }
}