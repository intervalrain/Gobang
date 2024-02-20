namespace Domain;

public enum Role
{
    Black,
    White,
    Audience
}


public static class RoleExtension
{
    public static ChessState ToChessState(this Role role)
    {
        switch (role)
        {
            case Role.Black:
                return ChessState.Black;
            case Role.White:
                return ChessState.White;
            default:
                throw new InvalidOperationException();
        }
    }
}