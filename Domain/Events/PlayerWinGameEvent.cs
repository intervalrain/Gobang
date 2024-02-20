using Domain.Common;

namespace Domain.Events;

public record PlayerWinGameEvent(string PlayerId) : DomainEvent;