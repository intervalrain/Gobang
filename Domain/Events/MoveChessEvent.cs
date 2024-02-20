using Domain.Common;

namespace Domain.Events;

public record MoveChessEvent(string GameId, string PlayerId, int Row, int Col) : DomainEvent;
public record FailToMoveChessEvent(string GameId, string PlayerId, int Row, int Col) : DomainEvent;
public record PassMoveChessEvent(string GameId, string PlayerId) : DomainEvent;