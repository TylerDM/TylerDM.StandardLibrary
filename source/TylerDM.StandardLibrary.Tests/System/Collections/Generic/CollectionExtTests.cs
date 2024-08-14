namespace TylerDM.StandardLibrary.System.Collections.Generic;

public class CollectionExtTests
{
	[Fact]
	public void AddRangeEnumerable()
	{
		var collection = new Collection<int>();
		collection.AddRange(new List<int>() { 1, 2, 3 });
		Assert.Collection(collection,
			i => Assert.Equal(1, i),
			i => Assert.Equal(2, i),
			i => Assert.Equal(3, i)
		);
	}

	[Fact]
	public void AddRangeParamsArray()
	{
		var collection = new Collection<int>();
		collection.AddRange(4, 5, 6);
		Assert.Collection(collection,
			i => Assert.Equal(4, i),
			i => Assert.Equal(5, i),
			i => Assert.Equal(6, i)
		);
	}
}
