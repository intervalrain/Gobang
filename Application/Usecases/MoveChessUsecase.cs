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

    public override Task ExecuteAsync(MoveChessRequest request, IPresenter<MoveChessResponse> presenter)
    {
        throw new NotImplementedException();
    }
}

