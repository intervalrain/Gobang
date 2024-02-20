using Application.DataModels;
using Domain.Rules;

namespace Application.Common;

public interface IRepository
{
	public Gobang FindGameById(string id);
	public string[] GetRooms();
	public bool HasRoom(string id);
	public string Save(Gobang gobang);
}

public interface ICommandRepository : IRepository
{
}

public interface IQueryRepository : IRepository
{
}

public static class RepositoryExtension
{
    public static string Save(this IRepository repository, Domain.Gobang domainGobang)
    {
        var game = domainGobang.ToApplication();
        return repository.Save(game);
    }

    private static Gobang ToApplication(this Domain.Gobang domainGobang)
    {
        return new Gobang(domainGobang.Id, domainGobang.Black.Id, domainGobang.Room.Password!);
    }

    public static Domain.Gobang ToDomain(this Gobang gobang)
    {
        return new Domain.Gobang(gobang.Id, gobang.Password, new Domain.Chess(), new Simple(), gobang.HostId);
    }
} 
