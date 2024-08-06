namespace TylerDM.StandardLibrary.System;

public static class IntExt
{
	public static int? ParseNullable(string? valueString)
	{
		if (int.TryParse(valueString, out var value)) return value;
		return null;
	}
}