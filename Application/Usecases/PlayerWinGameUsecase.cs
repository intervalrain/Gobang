using System;
using Application.Common;
using Domain.Common;

namespace Application.Usecases;

public record PlayerWinGameRequest(string GameId, string PlayerId) : Request(GameId, PlayerId);

public record PlayerWinGameResponse(IReadOnlyList<DomainEvent> Events) : CommandResponse(Events);

public class PlayerWinGameUsecase : CommandUsecase<PlayerWinGameRequest, PlayerWinGameResponse>
{
	public PlayerWinGameUsecase(ICommandRepository repository, IEventBus<DomainEvent> eventBus)
		: base(repository, eventBus)
	{
	}

    public override async Task ExecuteAsync(PlayerWinGameRequest request, IPresenter<PlayerWinGameResponse> presenter)
    {
        // 查
        var game = Repository.FindGameById(request.GameId).ToDomain();

        // 改
        game.PlayerWinGame(request.PlayerId);

        // 存
        Repository.Save(game);

        // 推
        await presenter.PresentAsync(new PlayerWinGameResponse(game.DomainEvents));
    }
}

