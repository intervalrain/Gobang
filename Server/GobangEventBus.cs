using Application.Common;
using Domain.Common;
using Server.Common;

namespace Server;

internal class GobangEventBus : IEventBus<DomainEvent>
{
    private readonly Dictionary<Type, IGobangEventHandler> _handlers; 

	public GobangEventBus(IEnumerable<IGobangEventHandler> handlers)
	{
        _handlers = handlers.ToDictionary(h => h.EventType, h => h);
	}

    public Task PublishAsync(IEnumerable<DomainEvent> events)
    {
        throw new NotImplementedException();
    }

    private IGobangEventHandler GetHandler(DomainEvent e)
    {
        var type = e.GetType();
        if (!_handlers.TryGetValue(type, out var handler))
        {
            throw new InvalidOperationException($"Handler for {type} not registered");
        }
        return handler;
    }
}

