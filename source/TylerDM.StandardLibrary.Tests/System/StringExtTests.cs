namespace TylerDM.StandardLibrary.System;

public class StringExtTests
{
	private static readonly string?[] _emptyStrings =
	[
		null,
		"",
		" ",
		"\t",
		"\n",
		"\r\n"
	];
	
	[Fact]
	public void HasContent()
	{
		Assert.True("Test".HasContent());
		foreach (var emptyString in _emptyStrings)
			Assert.False(emptyString.HasContent());
	}
	
	[Fact]
	public void IsEmpty()
	{
		Assert.False("Test".IsEmpty());
		foreach (var emptyString in _emptyStrings)
			Assert.True(emptyString.IsEmpty());
	}

	[Fact]
	public void RequireContent()
	{
		"Test".RequireContent();
		foreach (var emptyString in _emptyStrings)
			Assert.Throws<Exception>(emptyString.RequireContent);
	}
}
