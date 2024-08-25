namespace TylerDM.StandardLibrary.System;

public class StringExtTests
{
	[Fact]
	public void HasContent()
	{
		Assert.True("Test".HasContent());
		Assert.False("".HasContent());
		Assert.False(" ".HasContent());
		Assert.False("\t".HasContent());
		Assert.False("\n".HasContent());
		Assert.False("\r\n".HasContent());
	}

	[Fact]
	public void RequireContent()
	{
		"Test".RequireContent();
		Assert.Throws<Exception>("".RequireContent);
		Assert.Throws<Exception>(" ".RequireContent);
		Assert.Throws<Exception>("\t".RequireContent);
		Assert.Throws<Exception>("\n".RequireContent);
		Assert.Throws<Exception>("\r\n".RequireContent);
	}
}
