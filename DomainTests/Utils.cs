using Domain.Common;

namespace DomainTests;

public static class DomainEventExtension
{
    public static IEnumerable<DomainEvent> MoveNext(this IEnumerable<DomainEvent> domainEvents, DomainEvent e)
    {
        var first = domainEvents.First();
        Assert.AreEqual(e, first);
        return domainEvents.Skip(1);
    }

    public static void EndEvent(this IEnumerable<DomainEvent> domainEvents)
    {
        Assert.IsFalse(domainEvents.Any(), string.Join('\n', domainEvents));
    }
}