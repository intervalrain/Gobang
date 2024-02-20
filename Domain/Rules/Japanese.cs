using Domain.Interfaces;

namespace Domain.Rules;

/**
 * 【日本規則】
 * 1. 黑棋先行滿，黑白輪流
 * 2. 黑方有三三、四四及長連禁手(包含334、433)，在五連前無論主動被動下出禁著點，即為負。
 * 3. 白方無任何禁著點，長連亦可
 * 4. 五連與禁手同時發生時，不算禁手。
 * 5. 先連五者勝
 */
public class Japanese : IRule
{
    public GameState Judge(Chess chess, int row, int col)
    {
        throw new NotImplementedException();
    }
}

