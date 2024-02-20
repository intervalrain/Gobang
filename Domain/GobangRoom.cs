namespace Domain;

public class GobangRoom
{
    public Guid Guid { get; private set; }
    public string Host { get; private set; }
    public string RoomName { get; private set; }
    public string? Password { get; private set; }
    public List<Player> Players { get; private set; } = new();
    public int RoomStatus { get; private set; } = 0;

    public GobangRoom(string roomname, string host, string password, Role role = Role.Black)
    {
        Guid = Guid.NewGuid();
        Host = host;
        RoomName = roomname;
        Password = Convert.ToString(password.GetHashCode() % 2463 + 10785426);
        var player = new Player(host);
        player.Role = role;
        Players.Add(player);
        RoomStatus = (role == Role.Black) ? 1 : 2;
    }

    public bool Validate(string playerId, string? password)
    {
        if (password is null || playerId == Host) return false;
        var encrypted = Convert.ToString(password.GetHashCode() % 2463 + 10785426);
        return encrypted == Password;
    }

    public bool Join(string playerId, Role role)
    {
        var player = Players.First(p => p.Id == playerId);
        switch (RoomStatus)
        {
            case 0:
                player.Role = role;
                break;
            case 1:
                player.Role = Role.White;
                break;
            case 2:
                player.Role = Role.Black;
                break;
            case 3:
            default:
                throw new ArgumentOutOfRangeException();
        }
        return true;
    }
}

