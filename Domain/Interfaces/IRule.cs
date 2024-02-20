namespace Domain.Interfaces;

public interface IRule
{
	GameState Judge(Chess chess, int row, int col);
}

