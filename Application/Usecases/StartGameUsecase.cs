using Application.Common;
using Domain.Common;

namespace Application.Usecases;

public record StartGameRequest(string GameId, string BlackId, string WhiteId) : Request(GameId, BlackId);

public record StartGameResponse(IReadOnlyList<DomainEvent> Events) : CommandResponse(Events);

public class StartGameUsecase : CommandUsecase<StartGameRequest, StartGameResponse>
{
	public StartGameUsecase(ICommandRepository repository, IEventBus<DomainEvent> eventBus)
        : base(repository, eventBus)
	{
	}

    public override async Task ExecuteAsync(StartGameRequest request, IPresenter<StartGameResponse> presenter)
    {
        // 查
        var game = Repository.FindGameById(request.GameId).ToDomain();

        // 改
        game.NewGame();

        // 存
        Repository.Save(game);

        // 推
        await presenter.PresentAsync(new StartGameResponse(game.DomainEvents));
    }
}

