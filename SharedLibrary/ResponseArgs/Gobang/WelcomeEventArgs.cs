namespace SharedLibrary.ResponseArgs.Gobang;

public class WelcomeEventArgs : EventArgs
{
    public required string GameId;
    public required string PlayerId;
}