namespace Server.DataModels;

public record CreateGameBodyPayload(Player[] players);
public record Player(string Id, string Name);