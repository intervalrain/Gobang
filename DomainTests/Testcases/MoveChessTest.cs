using Domain;
using Domain.Events;
using Domain.Exceptions;

namespace DomainTests.Testcases;

[TestClass]
public class MoveChessTest
{
    [TestMethod]
    public void 玩家A落子到空白處()
    {
        // Arrange
        var host = new { Id = "A" };
        var guest = new { Id = "B" };
        var room = new { Id = "Test", Password = "password" };
        var game = new Gobang(room.Id, room.Password, host.Id);
        game.EnterGame(guest.Id, room.Password);

        // Act
        game.PlayerMoveChess(host.Id, 0, 0);

        // Assert
        Assert.AreEqual(guest.Id, game.CurrentPlayer.Id);
        Assert.AreEqual(ChessState.Black, game.Chess[0, 0]);
        game.DomainEvents.MoveNext(new StartGameEvent(room.Id, host.Id, guest.Id, 1))
                         .MoveNext(new MoveChessEvent(room.Id, host.Id, 0, 0))
                         .EndEvent();
    }

    [TestMethod]
    public void 玩家A落子到已有子處()
    {
        var host = new { Id = "A" };
        var guest = new { Id = "B" };
        var room = new { Id = "Test", Password = "password" };
        var game = new Gobang(room.Id, room.Password, host.Id);
        game.EnterGame(guest.Id, room.Password);
        game.Chess[0, 0] = ChessState.Black;
        game.Chess[0, 1] = ChessState.White;

        // Act
        Assert.ThrowsException<AlreadyHasChessException>(() => game.PlayerMoveChess(host.Id, 0, 1));

        // Assert
        Assert.AreEqual(host.Id, game.CurrentPlayer.Id);
        Assert.AreEqual(ChessState.Black, game.Chess[0, 0]);
        Assert.AreEqual(ChessState.White, game.Chess[0, 1]);
        game.DomainEvents.MoveNext(new StartGameEvent(room.Id, host.Id, guest.Id, 1))
                         .EndEvent();
    }

    [TestMethod]
    public void 玩家A在非自己的回合時落子()
    {
        var host = new { Id = "A" };
        var guest = new { Id = "B" };
        var room = new { Id = "Test", Password = "password" };
        var game = new Gobang(room.Id, room.Password, host.Id);
        game.EnterGame(guest.Id, room.Password);
        game.PlayerMoveChess(host.Id, 0, 0);

        // Act
        Assert.ThrowsException<NotPlayerRoundException>(() => game.PlayerMoveChess(host.Id, 0, 1));

        // Assert
        Assert.AreEqual(guest.Id, game.CurrentPlayer.Id);
        Assert.AreEqual(ChessState.Black, game.Chess[0, 0]);
        Assert.AreEqual(ChessState.Empty, game.Chess[0, 1]);
        game.DomainEvents.MoveNext(new StartGameEvent(room.Id, host.Id, guest.Id, 1))
                         .MoveNext(new MoveChessEvent(room.Id, host.Id, 0, 0))
                         .EndEvent();
    }
}

