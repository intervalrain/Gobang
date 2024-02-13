using Application.DataModels;

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