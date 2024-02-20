using Application.Usecases;
using Microsoft.AspNetCore.SignalR;
using Server.Models;
using Server.Presenters;
using SharedLibrary;

namespace Server.Hubs;

public class GobangHub : Hub<IGobangResponses>
{
	public const string HubUrl = "/gobang";

    private readonly List<GobangRoom> goBangRooms = new(); 

    public async Task CreateRoom(string roomName, string playerId, string? password, CreateRoomUsecase usecase, SignalrDefaultPresenter<CreateRoomResponse> presenter)
    {
        await usecase.ExecuteAsync(
            new CreateRoomRequest(roomName, playerId, password),
            presenter
            );
    }
    //public async Task CreateRoom(string roomName, string? password = null)
    //{
    //    goBangRooms.Add(new GobangRoom
    //    {
    //        Guid = Guid.NewGuid(),
    //        RoomName = roomName,
    //        Password = password
    //    });

    //    await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
    //}

    public async Task GetIntoRoom(string gameId, string playerId, string? password, GetIntoRoomUsecase usecase, SignalrDefaultPresenter<GetIntoRoomResponse> presenter)
    {
        await usecase.ExecuteAsync(
            new GetIntoRoomRequest(gameId, playerId, password),
            presenter
            );
    }
    //public async Task GetIntoRoom(string roomName, string? password = null)
    //{
    //    var room = goBangRooms.FirstOrDefault(m => m.RoomName == roomName);
    //    if (room is null)
    //    {
    //        await Clients.Caller.SendAsync("Alert", "房間不存在!");
    //        return;
    //    }
    //    else
    //    {
    //        if (!string.IsNullOrEmpty(room.Password) && room.Password != password)
    //        {
    //            await Clients.Caller.SendAsync("Alert", "房間密碼錯誤!");
    //            return;
    //        }
    //    }
    //    await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
    //}

    public async Task Playing(string gameId, string playerId, int row, int col, MoveChessUsecase usecase, SignalrDefaultPresenter<MoveChessResponse> presenter)
    {
        await usecase.ExecuteAsync(
            new MoveChessRequest(gameId, playerId, row, col),
            presenter
            );
    }
    //public async Task Playing(GobangRoom room, int[,] chess, int row, int cell, int blackOrWhite)
    //{
    //    goBangRooms.First(m => m.Guid == room.Guid).Chess = chess;
    //    await Clients.OthersInGroup(room.RoomName).SendAsync("Playing", row, cell, blackOrWhite);
    //}

    public async Task Win(string gameId, string playerId, PlayerWinGameUsecase usecase, SignalrDefaultPresenter<PlayerWinGameResponse> presenter)
    {
        await usecase.ExecuteAsync(
            new PlayerWinGameRequest(gameId, playerId),
            presenter
            );
    }
    //public async Task Win(GobangRoom room)
    //{
    //    await Clients.OthersInGroup(room.RoomName).SendAsync("Alert", "\n遊戲結束!");
    //}

    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"{Context.ConnectionId} connected");
        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? e)
    {
        Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
        await base.OnDisconnectedAsync(e);
    }
}

