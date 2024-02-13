using Application.DataModels;

namespace ServerTests.AcceptanceTests;

[TestClass]
public class MoveChessTest
{
	private GobangTestServer _server = default!;

	[TestInitialize]
	public void Setup()
	{
		_server = new GobangTestServer();
	}
}

