using Application.Usecases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Server.DataModels;
using Server.Services;
using ServerTests.Usecases;
using SharedLibrary;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;

namespace ServerTests;

internal class GobangTestServer : WebApplicationFactory<Program>
{
    public HttpClient Client { get; }

    public GobangTestServer()
    {
        Client = CreateClient();
    }

    public T GetRequiredService<T>()
            where T : notnull
    {
        return Server.Services.CreateScope().ServiceProvider.GetRequiredService<T>();
    }

    public async Task<VerificationHub> CreateHubConnectionAsync(string gameId, string playerId)
    {
        var uri = new UriBuilder(Client.BaseAddress!)
        {
            Path = $"/gobang",
            Query = $"gameid={gameId}"
        }.Uri;
        var hub = new HubConnectionBuilder()
            .WithUrl(uri, opt =>
            {
                opt.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.ServerSentEvents;
                opt.AccessTokenProvider = async () =>
                {
                    var options = GetRequiredService<IOptionsMonitor<JwtBearerOptions>>();

                    var jwtToken = GetRequiredService<MockJwtTokenService>()
                        .GenerateJwtToken(options.Get("Bearer").Audience, playerId);
                    return await Task.FromResult(jwtToken);
                };
                opt.HttpMessageHandlerFactory = _ => Server.CreateHandler();
            })
            .Build();
        VerificationHub verificationHub = new(hub);
        await hub.StartAsync();

        return verificationHub;
    }


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton<MockJwtTokenService>();
            services.RemoveAll<IPlatformService>();
            services.RemoveAll<IOptions<JwtBearerOptions>>();

            services.AddSingleton<IPlatformService, MockPlatformService>();
            services.AddOptions<JwtBearerOptions>("Bearer")
                .Configure<MockJwtTokenService>((options, jwtToken) =>
                {
                    var config = new OpenIdConnectConfiguration()
                    {
                        Issuer = jwtToken.Issuer
                    };

                    config.SigningKeys.Add(jwtToken.SecurityKey);
                    options.Configuration = config;
                    options.Authority = null;
                });

            services.RemoveAll<MoveChessUsecase>();
            services.AddScoped<MoveChessUsecase, MockMoveChessUsecase>();
        });
    }
}