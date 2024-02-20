namespace Domain.Exceptions;

public class InvalidChessPositionException : Exception
{
	public InvalidChessPositionException(string message)
		: base(message)
	{
	}
}

