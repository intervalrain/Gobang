using Microsoft.AspNetCore.Components;

namespace Server.Components;

public partial class Chessboard
{
    [Parameter]
    public int[,] Chess { get; set; }
    [Parameter]
    public EventCallback<(int, int)> OnPlaying { get; set; }

    private async Task Playing(int row, int cell)
    {
        if (OnPlaying.HasDelegate)
        {
            await OnPlaying.InvokeAsync((row, cell));
        }
    }
}

