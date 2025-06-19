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

	[Fact]
	public void Truncate()
	{
		var text = "12345";
		Assert.Equal("123", text.Truncate(3));
		Assert.Equal("12345", text.Truncate(5));
		Assert.Equal("12345", text.Truncate(10));
		Assert.Equal("", text.Truncate(0));
		Assert.Same(text, text.Truncate(100));
	}
}
