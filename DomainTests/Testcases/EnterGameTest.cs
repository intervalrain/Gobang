using Domain;
using Domain.Events;

namespace DomainTests.Testcases;

[TestClass]	
public class EnterGameTest
{
	[TestMethod]
	public void 玩家B進入房間_輸入正確密碼()
	{
		// Arrange
		var host = new { Id = "A" };
		var room = new { Id = "Test", Password = "password" };
		var guest = new { Id = "B" };
		var invitation = new { Id = "Test", Password = "password" };

		var game = new Gobang(room.Id, room.Password, host.Id);

		// Act
		game.EnterGame(guest.Id, invitation.Password);

		// Assert
		Assert.AreEqual(game.Black.Id, host.Id);
		Assert.AreEqual(game.White.Id, guest.Id);
		Assert.AreEqual(game.CurrentPlayer.Id, host.Id);
		game.DomainEvents.MoveNext(new StartGameEvent(room.Id, host.Id, guest.Id, 1))
						 .EndEvent();
    }

    [TestMethod]
    public void 玩家A進入房間_與房主撞名()
    {
        // Arrange
        var host = new { Id = "A" };
        var room = new { Id = "Test", Password = "password" };
        var guest = new { Id = "A" };
        var invitation = new { Id = "Test", Password = "password" };

        var game = new Gobang(room.Id, room.Password, host.Id);

        // Act
        game.EnterGame(guest.Id, invitation.Password);

        // Assert
        Assert.AreEqual(game.Black.Id, host.Id);
        Assert.IsNull(game.White);
        Assert.IsNull(game.CurrentPlayer);
        game.DomainEvents.MoveNext(new FailToGetIntoRoomEvent(room.Id, invitation.Password, guest.Id))
                         .EndEvent();
    }

    [TestMethod]
	public void 玩家B進入房間_輸入錯誤密碼()
	{
        // Arrange
        var host = new { Id = "A" };
        var room = new { Id = "Test", Password = "password" };
        var guest = new { Id = "B" };
        var invitation = new { Id = "Test", Password = "wordpass" };

        var game = new Gobang(room.Id, room.Password, host.Id);

        // Act
        game.EnterGame(guest.Id, invitation.Password);

        // Assert
        Assert.AreEqual(game.Black.Id, host.Id);
        Assert.IsNull(game.White);
        Assert.IsNull(game.CurrentPlayer);
        game.DomainEvents.MoveNext(new FailToGetIntoRoomEvent(room.Id, invitation.Password, guest.Id))
                         .EndEvent();
    }
}