using Domain.Common;

namespace Domain.Events;

public record GetIntoRoomEvent(string GameId, string? Password, string PlayerId) : DomainEvent;
public record RoomNotExistEvent(string GameId, string? Password, string PlayerId) : DomainEvent;
public record FailToGetIntoRoomEvent(string GameId, string? Password, string PlayerId) : DomainEvent;