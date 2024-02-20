using Domain.Interfaces;

namespace Domain.Rules;

/**
 * 【簡易規則】
 * 1. 黑棋先行滿，黑白輪流
 * 2. 先連五者勝(包含以上)
 */
public class Simple : IRule
{
    private readonly (int, int)[] dirPairs = new (int, int)[4]
    {
        (0,1),
        (1,1),
        (1,0),
        (1,-1)
    };
    public GameState Judge(Chess chess, int row, int col)
    {
        foreach (var dir in dirPairs)
        {
            int left = Dfs(chess, row, col, dir);
            int right = Dfs(chess, row, col, new(-dir.Item1, -dir.Item2));
            int sum = left + 1 + right;
            if (sum >= 5) return GameState.Win;
        }
        return GameState.None;
    }

    private static int Dfs(Chess chess, int row, int col, (int, int) dir)
    {
        int newrow = row + dir.Item1;
        int newcol = col + dir.Item2;
        if (!chess.IsValid(newrow, newcol)) return 0;
        if (chess[newrow, newcol] != chess[row, col]) return 0;
        return Dfs(chess, newrow, newcol, dir) + 1;
    }
}

