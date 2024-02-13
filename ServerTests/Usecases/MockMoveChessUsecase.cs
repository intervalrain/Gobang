using Application.Common;
using Application.Usecases;
using Domain.Common;

namespace ServerTests.Usecases;

public class MockMoveChessUsecase : MoveChessUsecase
{
    public MockMoveChessUsecase(ICommandRepository repository, IEventBus<DomainEvent> eventBus)
        : base(repository, eventBus)
    {
    }

    public override async Task ExecuteAsync(MoveChessRequest request, IPresenter<MoveChessResponse> presenter)
    {
        // 查
        var game = Repository.FindGameById(request.GameId).ToDomain();

        // 改
        game.MoveChess(request.PlayerId, request.Row, request.Col);

        // 存
        Repository.Save(game);

        // 推
        await presenter.PresentTask(new MoveChessResponse(game.DomainEvents));
    }
}

