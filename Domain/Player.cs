namespace Domain;

public class Player
{
    public string Id { get; private set; }
    public Role Role { get; set; }

    public Player(string id)
    {
        Id = id;
    }
}

