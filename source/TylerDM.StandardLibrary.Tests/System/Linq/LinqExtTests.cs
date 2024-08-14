namespace TylerDM.StandardLibrary.System.Linq;

public static class LinqExtTests
{
	class TestSelectFollowClass() { public TestSelectFollowClass? Next { get; init; } }

	[Fact]
	public static void SelectWithSurrounding()
	{
		int[] numbers = [1, 2, 3];
		var enumerator = numbers.SelectWithSurroundingStruct().GetEnumerator();
		moveAndValidate(true);
		int? previous, current, next;

		void validate(int? expectedPrev, int? expectedCurrent, int? expectedNext)
		{
			if (previous != expectedPrev)
				throw new Exception("Previous was incorrect.");
			if (current != expectedCurrent)
				throw new Exception("Current was incorrect.");
			if (next != expectedNext)
				throw new Exception("Next was incorrect.");
		}

		void moveAndValidate(bool expected)
		{
			if (enumerator.MoveNext() != expected)
				throw new Exception("Incorrect enumerable length");
		}

		(previous, current, next) = enumerator.Current;
		moveAndValidate(true);
		validate(null, 1, 2);

		(previous, current, next) = enumerator.Current;
		moveAndValidate(true);
		validate(1, 2, 3);

		(previous, current, next) = enumerator.Current;
		moveAndValidate(false);
		validate(2, 3, null);
	}

	[Fact]
	public static void SelectFollow()
	{
		var chain = new TestSelectFollowClass() { Next = new() { Next = new() } };
		var array = chain.SelectFollow(x => x.Next).ToArray();
		if (array.Length != 3)
			throw new Exception("Incorrect number of elements returned.");
	}

	[Fact]
	public static void SelectMany()
	{
		var array = new int[][] { [0], [0, 0] };
		var flattened = array.SelectMany().ToArray();
		if (flattened.Length != 3)
			throw new Exception();
	}
}
