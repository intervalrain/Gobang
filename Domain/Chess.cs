using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain;

public class Chess
{
	public int Size { get; private set; }
	public ChessState[,] Grid { get; private set; } 

	public Chess(int size = 19)
	{
		Size = size;
		Grid = new ChessState[size, size];
        Reset();
	}

    public void Reset()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Grid[i, j] = ChessState.Empty;
            }
        }
    }

	public ChessState this[int row, int col]
	{
		get
		{
			return GetChessState(row, col);
		}
        set
        {
            SetChessState(row, col, value);
        }
	}

	public bool IsValid(int row, int col)
	{
		int center = Size >> 1;
		int newrow = row + center;
		int newcol = col + center;
		return newrow >= 0 && newrow < Size && newcol >= 0 && newcol < Size;
	}

	private (int, int) ToAbsolute(int row, int col)
	{
        int center = Size >> 1;
        int newrow = row + center;
        int newcol = col + center;
        if (newrow < 0 || newrow >= Size || newcol < 0 || newcol >= Size)
            throw new InvalidChessPositionException($"Invalid position: [{row},{col}]");
		return (newrow, newcol);
    }

    private ChessState GetChessState(int row, int col)
    {
        var (newrow, newcol) = ToAbsolute(row, col);
        return Grid[newrow, newcol];
    }

    private void SetChessState(int row, int col, ChessState state)
    {
        var (newrow, newcol) = ToAbsolute(row, col);
        if (Grid[newrow, newcol] != ChessState.Empty)
        {
            throw new AlreadyHasChessException($"[{row},{col}] has already been put.");
        }
        Grid[newrow, newcol] = state;
    }

    public GameState AddMove(Player player, int row, int col, IRule rule)
    {
        ChessState state = ChessState.Empty;
        if (player.Role == Role.Black)
        {
            state = ChessState.Black;
        }
        else
        {
            state = ChessState.White;
        }

        SetChessState(row, col, state);
        return rule.Judge(this, row, col);
    }
}

