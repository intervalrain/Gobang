using Application.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
	public static IServiceCollection AddGobangApplication(this IServiceCollection services)
	{
		return services.AddUsecases();
	}

	private static IServiceCollection AddUsecases(this IServiceCollection services)
	{
		var assembly = typeof(DependencyInjection).Assembly;
		var types = assembly.GetTypes();
		var usecaseType = typeof(CommandUsecase<,>);
		var queryUsecaseType = typeof(QueryUsecase<,>);

		foreach (var type in types.Where(t => t.BaseType?.IsGenericType is true && t.IsAbstract == false))
		{
			if (type.BaseType?.GetGenericTypeDefinition() == usecaseType)
			{
				services.AddTransient(type, type);
			}
			else if (type.BaseType?.GetGenericTypeDefinition() == queryUsecaseType)
			{
                services.AddTransient(type, type);
            }
		}
		return services;
	}
}

