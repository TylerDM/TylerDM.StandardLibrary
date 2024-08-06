namespace TylerDM.StandardLibrary.System.Text;

public static class StringBuilderExt
{
	public static void RemoveFromEnd(this StringBuilder stringBuilder, int length) =>
		stringBuilder.Remove(stringBuilder.Length - length, length);
}
