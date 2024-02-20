namespace Domain.Exceptions;

public class AlreadyHasChessException : Exception
{
	public AlreadyHasChessException(string message)
		: base(message)
	{
	}
}

