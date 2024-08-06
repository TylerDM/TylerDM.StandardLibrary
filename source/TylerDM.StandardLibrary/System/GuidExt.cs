namespace TylerDM.StandardLibrary.System;

public static class GuidExt
{
	public static Guid? ParseNullable(string? text)
	{
		if (Guid.TryParse(text, out var value)) return value;
		return null;
	}
}
