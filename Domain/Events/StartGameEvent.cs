using Domain.Common;

namespace Domain.Events;

public record StartGameEvent(string GameId, string BlackId, string WhiteId, int Rounds) : DomainEvent;