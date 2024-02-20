using Domain.Interfaces;

namespace Domain.Rules;

/**
 * 【標準規則】
 * 1. 黑棋先行滿，黑白輪流
 * 2. 長連禁止
 * 3. 先連五者勝(不含以上)
 */
public class Standard : IRule
{
    public GameState Judge(Chess chess, int row, int col)
    {
        throw new NotImplementedException();
    }
}

