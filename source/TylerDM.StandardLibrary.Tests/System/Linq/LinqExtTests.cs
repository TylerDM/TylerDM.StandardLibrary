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

	[Fact]
	public static void TestSelectMany()
	{
		var array = new int[][] { [0], [0, 0] };
		var flattened = array.SelectMany().ToArray();
		if (flattened.Length != 3)
			throw new Exception();
	}
}
