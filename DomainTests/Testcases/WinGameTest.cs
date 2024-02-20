using Domain;
using Domain.Events;

namespace DomainTests.Testcases;

[TestClass]
public class WinGameTest
{
    [TestMethod]
    public void 玩家A連5獲勝()
    {
        // Arrange
        var host = new { Id = "A" };
        var guest = new { Id = "B" };
        var room = new { Id = "Test", Password = "password" };
        var game = new Gobang(room.Id, room.Password, host.Id);
        game.EnterGame(guest.Id, room.Password);
        game.Chess[0, 0] = ChessState.Black;
        game.Chess[0, 1] = ChessState.White;
        game.Chess[1, 0] = ChessState.Black;
        game.Chess[0, 2] = ChessState.White;
        game.Chess[2, 0] = ChessState.Black;
        game.Chess[0, 3] = ChessState.White;
        game.Chess[3, 0] = ChessState.Black;
        game.Chess[0, 4] = ChessState.White;

        // Act
        game.PlayerMoveChess(host.Id, 4, 0);

        // Assert
        game.DomainEvents.MoveNext(new StartGameEvent(room.Id, host.Id, guest.Id, 1))
                         .MoveNext(new MoveChessEvent(room.Id, host.Id, 4, 0))
                         .MoveNext(new PlayerWinGameEvent(host.Id))
                         .EndEvent();
    }
}

