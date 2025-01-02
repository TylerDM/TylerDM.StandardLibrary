using System.ComponentModel.DataAnnotations;

namespace TylerDM.StandardLibrary.System;

public class EnumExtTests
{
	enum GetNameEnum
	{
		[Display(Name = "")]
		Blank,
		[Display(Name = "1")]
		One,
		Two
	}

	enum EnumTwo
	{
		TotallyNotBlank = GetNameEnum.Blank,
		TotallyNotOne = GetNameEnum.One,
		TotallyNotTwo = GetNameEnum.Two,
	}

	[Fact]
	public void ToEnum()
	{
		Assert.Equal(EnumTwo.TotallyNotBlank, GetNameEnum.Blank.ToEnum<EnumTwo>());
		Assert.NotEqual(EnumTwo.TotallyNotOne, GetNameEnum.Blank.ToEnum<EnumTwo>());
	}

	[Fact]
	public void RequireDefined()
	{
		Assert.ThrowsAny<Exception>(() => ((GetNameEnum)999).RequireDefined());
		GetNameEnum.Blank.RequireDefined();
	}

	[Fact]
	public void IsDefined()
	{
		Assert.False(((GetNameEnum)999).IsDefined());
		Assert.True(GetNameEnum.Blank.IsDefined());
	}
	
	[Fact]
	public void GetName()
	{
		Assert.Equal("", GetNameEnum.Blank.GetName());
		Assert.Equal("1", GetNameEnum.One.GetName());
		Assert.Equal("Two", GetNameEnum.Two.GetName());
	}

	enum GetDescEnum
	{
		[Display(Description = "")]
		Blank,
		[Display(Description = "1Desc")]
		One,
		Two
	}

	[Fact]
	public void GetDescription()
	{
		Assert.Equal("", GetDescEnum.Blank.GetDescription());
		Assert.Equal("1Desc", GetDescEnum.One.GetDescription());
		Assert.Equal("Two", GetDescEnum.Two.GetDescription());
	}
}
