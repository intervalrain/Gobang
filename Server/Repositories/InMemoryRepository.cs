using Application.Common;
using Application.DataModels;

namespace Server.Repositories;

public class InMemoryRepository : ICommandRepository, IQueryRepository
{
    private readonly Dictionary<string, Gobang> Games = new();  

    public Gobang FindGameById(string id)
    {
        if (!Games.TryGetValue(id, out Gobang? game))
        {
            throw new GameNotFoundException(id);
        }
        return game;
    }

    public string[] GetRooms()
    {
        return Games.Keys.ToArray();
    }

    public bool HasRoom(string id)
    {
        return Games.ContainsKey(id);
    }

    public string Save(Gobang gobang)
    {
        var game = gobang with { Id = (Games.Count + 1).ToString() };
        Games[game.Id] = game;
        return game.Id;
    }
}

