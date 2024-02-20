using Application.Common;
using Domain.Common;

namespace Application.Usecases;

public record MoveChessRequest(string GameId, string PlayerId, int Row, int Col) : Request(GameId, PlayerId);

public record MoveChessResponse(IReadOnlyList<DomainEvent> Events) : CommandResponse(Events);

public class MoveChessUsecase : CommandUsecase<MoveChessRequest, MoveChessResponse>
{
	public MoveChessUsecase(ICommandRepository repository, IEventBus<DomainEvent> eventBus)
		: base(repository, eventBus)
	{  
	}

    public override async Task ExecuteAsync(MoveChessRequest request, IPresenter<MoveChessResponse> presenter)
    {
		// 查
		var game = Repository.FindGameById(request.GameId).ToDomain();

		// 改
		game.PlayerMoveChess(request.PlayerId, request.Row, request.Col);

		// 存
		Repository.Save(game);

		// 推
		await presenter.PresentAsync(new MoveChessResponse(game.DomainEvents));
    }
}

