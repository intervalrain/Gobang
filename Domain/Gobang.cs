using Domain.Common;

namespace Domain;

public class Gobang : AbstractAggregateRoot
{
    public string Id { get; set; }

    public void MoveChess(string playerId, int row, int col)
    {

    }
}

