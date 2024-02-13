using Server.DataModels;

namespace Server.Services;

public class DevelopmentPlatformService : IPlatformService
{
    private readonly IConfiguration _configuration;

    public DevelopmentPlatformService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<UserInfo> GetUserInfo(string tokenString)
    {
        var usersSection = _configuration.GetSection("Authentication:Users");
        var users = usersSection.GetChildren();
        foreach (var user in users)
        {
            if (user["Token"] != tokenString) continue;

            var id = user["Id"];
            var email = user["Email"];
            var name = user["Name"];
            var userInfo = new UserInfo(id!, email!, name!);
            return Task.FromResult(userInfo);
        }
        throw new Exception("找不到使用者資訊");
    }

    public (string Id, string Token)[] GetUsers()
    {
        var usersSection = _configuration.GetSection("Authentication:Users");
        var users = usersSection.GetChildren();
        var usersInfo = new List<(string Id, string Token)>();
        foreach (var user in users)
        {
            var id = user["Id"];
            var token = user["Token"];
            usersInfo.Add((id!, token!));
        }
        return usersInfo.ToArray();
    }
}

