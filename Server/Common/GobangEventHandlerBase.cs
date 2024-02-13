using Domain.Common;

namespace Server.Common;

public abstract class GobangEventHandlerBase<TEvent> : IGobangEventHandler where TEvent : DomainEvent
{
	public Type EventType => typeof(TEvent);
	
	public Task HandleAsync(DomainEvent e)
	{
		if (e is TEvent tEvent)
		{
			return HandleSpecificEvent(tEvent);
        }
		throw new InvalidOperationException($"Handler for {e.GetType()} not registered");
	}

	protected abstract Task HandleSpecificEvent(TEvent e);
}

