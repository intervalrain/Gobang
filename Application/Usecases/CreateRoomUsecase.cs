using Application.Common;
using Application.DataModels;
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

    public override async Task ExecuteAsync(CreateRoomRequest request, IPresenter<CreateRoomResponse> presenter)
    {
        // 查
        // 改
        var game = new Gobang(request.GameId, request.PlayerId, request.Password!).ToDomain();

        // 存
        Repository.Save(game);

        // 推
        await presenter.PresentAsync(new CreateRoomResponse(game.DomainEvents));
    }
}

