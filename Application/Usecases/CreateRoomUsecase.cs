using Application.Common;
using Domain.Common;

namespace Application.Usecases;

public record CreateRoomRequest(string GameId, string PlayerId, string? Password) : Request(GameId, PlayerId);

public record CreateRoomResponse(IReadOnlyList<DomainEvent> Events) : CommandResponse(Events);

public class CreateRoomUsecase : CommandUsecase<CreateRoomRequest, CreateRoomResponse>
{
	public CreateRoomUsecase(ICommandRepository repository, IEventBus<DomainEvent> eventBus)
        : base(repository, eventBus)
	{
	}

    public override Task ExecuteAsync(CreateRoomRequest request, IPresenter<CreateRoomResponse> presenter)
    {
        throw new NotImplementedException();
    }
}

