using Application.Common;
using Domain.Common;

namespace Server.Presenters;

public class SignalrDefaultPresenter<TResponse> : IPresenter<TResponse> where TResponse : CommandResponse
{
    private IEventBus<DomainEvent> _eventBus;

	public SignalrDefaultPresenter(IEventBus<DomainEvent> eventBus)
	{
	}

    public async Task PresentTask(TResponse response)
    {
        await _eventBus.PublishAsync(response.Events);
    }
}