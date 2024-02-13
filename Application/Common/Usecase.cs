using Domain.Common;

namespace Application.Common;

public abstract class Usecase<TRequest, TResponse> where TRequest : Request where TResponse : Response
{
	public abstract Task ExecuteAsync(TRequest request, IPresenter<TResponse> presenter);
}

public abstract class CommandUsecase<TRequest, TResponse> : Usecase<TRequest, TResponse> where TRequest : Request where TResponse : Response
{
	protected ICommandRepository Repository { get; }
	protected IEventBus<DomainEvent> EventBus { get; } 

	public CommandUsecase(ICommandRepository repository, IEventBus<DomainEvent> eventBus)
	{
		Repository = repository;
		EventBus = eventBus;
	}
}

public abstract class QueryUsecase<TRequest, TResponse> where TRequest : Request where TResponse : Response
{
	protected ICommandRepository Repository { get; }

	public QueryUsecase(ICommandRepository repository)
	{
		Repository = repository;
	}

	public abstract Task ExecuteAsync(TRequest request, IPresenter<TResponse> presenter);
}

