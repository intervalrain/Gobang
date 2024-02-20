using Domain.Common;

namespace Domain.Events;

public record CreateRoomEvent(string GameId, string PlayerId, string Password) : DomainEvent;
public record RoomHasExistedEvent(string GameId, string PlayerId) : DomainEvent;