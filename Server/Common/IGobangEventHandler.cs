using Domain.Common;

namespace Server.Common;

public interface IGobangEventHandler
{
	public Type EventType { get; }
    Task HandleAsync(DomainEvent e);
}

