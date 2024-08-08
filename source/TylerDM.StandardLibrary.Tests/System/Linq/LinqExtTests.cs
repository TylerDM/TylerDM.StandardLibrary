namespace TylerDM.StandardLibrary.System.Linq;

public static class LinqExtTests
{
	class TestSelectFollowClass() { public TestSelectFollowClass? Next { get; init; } }

	[Fact]
	public static void TestSelectFollow()
	{
		var chain = new TestSelectFollowClass() { Next = new() { Next = new() } };
		var array = chain.SelectFollow(x => x.Next).ToArray();
		if (array.Length != 3)
			throw new Exception("Incorrect number of elements returned.");
	}
}
