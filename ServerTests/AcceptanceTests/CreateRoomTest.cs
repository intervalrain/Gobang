using System.Net.Http.Headers;
using System.Net.Http.Json;
using Application.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Server.DataModels;

namespace ServerTests.AcceptanceTests;

[TestClass]
public class CreateRoomTest
{
	private GobangTestServer server = default!;
	private IRepository repository = default!;
	private MockJwtTokenService jwtTokenService = default!;
	private JwtBearerOptions jwtBearerOptions = default!;

	[TestInitialize]
	public void Setup()
	{
		server = new GobangTestServer();
		jwtTokenService = server.GetRequiredService<MockJwtTokenService>();
		repository = server.GetRequiredService<IQueryRepository>();
		jwtBearerOptions = server.GetRequiredService<IOptionsMonitor<JwtBearerOptions>>().Get("Bearer");
	}

	[TestMethod]
	public async void 玩家A新建房間()
	{
		CreateGameBodyPayload bodyPayload = new(new Player[]
		{
			new Player("A", "black"),
			new Player("B", "white"),
		});

		var jwt = jwtTokenService.GenerateJwtToken(jwtBearerOptions.Audience, "A");
		string gameId = "1";
		string expected = $"https://localhost:5005/gobang/{gameId}";

		// Act
		server.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
		HttpResponseMessage? response = await server.Client.PostAsJsonAsync("/gobang", bodyPayload);

		// Assert
		var data = await response.Content.ReadAsStringAsync();
		Assert.AreEqual(expected, data);
		Assert.IsTrue(repository.HasRoom(gameId));
		var game = repository.FindGameById(gameId);
		Assert.AreEqual("A", game.HostId);
	}
}

