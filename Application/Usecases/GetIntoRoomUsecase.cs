using Application.Common;
using Domain.Common;

namespace Application.Usecases;

public record GetIntoRoomRequest(string GameId, string PlayerId, string? Password) : Request(GameId, PlayerId);

public record GetIntoRoomResponse(IReadOnlyList<DomainEvent> Events) : CommandResponse(Events);

public class GetIntoRoomUsecase : CommandUsecase<GetIntoRoomRequest, GetIntoRoomResponse>
{
    public GetIntoRoomUsecase(ICommandRepository repository, IEventBus<DomainEvent> eventBus)
        : base(repository, eventBus)
    {
    }

    public override async Task ExecuteAsync(GetIntoRoomRequest request, IPresenter<GetIntoRoomResponse> presenter)
    {
        // 查
        var game = Repository.FindGameById(request.GameId).ToDomain();

        // 改
        game.EnterGame(request.PlayerId, request.Password);

        // 存
        Repository.Save(game);

        // 推
        await presenter.PresentAsync(new GetIntoRoomResponse(game.DomainEvents));
    }
}

