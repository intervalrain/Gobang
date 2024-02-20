using System.Reflection;
using Application.Common;
using Domain.Common;
using Server.Common;
using Server.Pages;
using Server.Presenters;
using Server.Repositories;
using SharedLibrary;

namespace Server;

public static class DependencyInjection
{
	public static IServiceCollection AddGobangServer(this IServiceCollection services)
	{
		var repository = new InMemoryRepository();
		services.AddSingleton<ICommandRepository>(repository)
				.AddSingleton<IQueryRepository>(repository)
				.AddSingleton<IEventBus<DomainEvent>, GobangEventBus>()
				.AddTransient(typeof(SignalrDefaultPresenter<>), typeof(SignalrDefaultPresenter<>));
		services.AddSignalREventHandlers();
		return services;
	}

	private static IServiceCollection AddSignalREventHandlers(this IServiceCollection services)
	{
		var handlers = Assembly.GetExecutingAssembly().GetTypes()
			.Where(t => t is { IsClass: true, IsAbstract: false } && t.IsAssignableTo(typeof(IGobangEventHandler)))
			.ToList();

		foreach (var handler in handlers)
		{
			services.AddSingleton(typeof(IGobangEventHandler), handler);
		}
		return services;
	}
}

