using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Server.Services;

namespace Server;

public class PlatformJwtEvents : JwtBearerEvents
{
    public override Task MessageReceived(MessageReceivedContext context)
    {
        var queryToken = context.Request.Query["access_token"].ToString();
        var headerToken = context.HttpContext.Request.Headers.Authorization.FirstOrDefault()?.Replace("Bearer ", "");

        var token = string.IsNullOrEmpty(queryToken) ? headerToken : queryToken;

        var path = context.HttpContext.Request.Path;

        if (!string.IsNullOrEmpty(token) && path.StartsWithSegments("gobang"))
        {
            context.Token = token;
        }
        return Task.CompletedTask;
    }

    public override async Task TokenValidated(TokenValidatedContext context)
    {
        var token = context.SecurityToken as JsonWebToken;
        var tokenString = token!.EncodedToken;
        var platformService = context.HttpContext.RequestServices.GetRequiredService<IPlatformService>();

        var userInfo = await platformService.GetUserInfo(tokenString);

        if (context.Principal?.Identity is ClaimsIdentity identity)
        {
            identity.AddClaim(new(ClaimTypes.Sid, userInfo!.Id));
            identity.AddClaim(new(ClaimTypes.Name, userInfo!.Name));
        }
    }
}

