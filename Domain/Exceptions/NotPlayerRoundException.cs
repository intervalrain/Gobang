namespace Domain.Exceptions;

public class NotPlayerRoundException : Exception
{
	public NotPlayerRoundException(string playerId)
		: base(playerId)
	{
	}
}

