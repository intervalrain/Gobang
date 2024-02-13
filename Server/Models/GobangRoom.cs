namespace Server.Models;

public class GobangRoom
{
    public Guid Guid { get; set; }
    public string RoomName { get; set; }
    public string? Password { get; set; }
    public int[,] Chess { get; set; } = new int[19, 19];
}

