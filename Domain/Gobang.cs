using Domain.Common;
using Domain.Events;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Rules;

namespace Domain;

public class Gobang : AbstractAggregateRoot
{
    public string Id { get; private set; }
    public GobangRoom Room { get; private set; } 
    public Chess Chess { get; private set; }
    public Player? Black { get; private set; }
    public Player? White { get; private set; }
    public IRule Rule { get; set; }
    public Player? CurrentPlayer { get; private set; }
    public int Rounds { get; private set; }

    public Gobang(string id, string password, Chess chess, IRule rule, string hostId)
    {
        Id = id;
        Chess = chess;
        CreateRoom(id, password, hostId);
        Rule = rule;
        Black = new Player(hostId, Role.Black);
        Rounds = 1;
    }

    public Gobang(string id, string password, string hostId)
    {
        Id = id;
        Chess = new Chess();
        Rule = new Simple();
        Black = new Player(hostId, Role.Black);
        Rounds = 1;
    }

    public void CreateRoom(string roomId, string password, string hostId)
    {
        Room = new GobangRoom(roomId, hostId, password);
        AddDomainEvent(new CreateRoomEvent(roomId, hostId, password));
    }

    public void EnterGame(string playerId, string? password)
    {
        if (password is null || !Room.Validate(playerId, password))
        {
            AddDomainEvent(new FailToGetIntoRoomEvent(Id, password, playerId));
        }
        else
        {
            CurrentPlayer = Black;
            White = new Player(playerId, Role.White);
            AddDomainEvent(new StartGameEvent(Id, Black.Id, White.Id, Rounds));
        }
    }

    public void NewGame()
    {
        Chess.Reset();
        Rounds++;
        AddDomainEvent(new StartGameEvent(Id, Black.Id, White.Id, Rounds));
    }

    public void PlayerMoveChess(string playerId, int row, int col)
    {
        var player = FindPlayerById(playerId);
        if (CurrentPlayer != player)
        {
            throw new NotPlayerRoundException(playerId);
        }
        GameState state = Chess.AddMove(player, row, col, Rule);
        AddDomainEvent(new MoveChessEvent(Id, CurrentPlayer.Id, row, col));
        if (state != GameState.None)
        {
            var winner = state == GameState.Win ? CurrentPlayer : SwapPlayer(CurrentPlayer);
            AddDomainEvent(new PlayerWinGameEvent(winner.Id));
        }
        else
        {
            CurrentPlayer = SwapPlayer(CurrentPlayer);
        }
    }

    public void PlayerWinGame(string winner)
    {
        AddDomainEvent(new PlayerWinGameEvent(winner));
    }

    private Player FindPlayerById(string playerId)
    {
        if (playerId == Black.Id)
            return Black;
        else if (playerId == White.Id)
            return White;
        throw new ArgumentOutOfRangeException(playerId);
    }

    private Player SwapPlayer(Player player)
    {
        return player == Black ? White : Black; 
    }
}

