namespace TylerDM.StandardLibrary.System;

public static class EnumExt
{
	#region methods
	public static T? ParseNullable<T>(this string value)
		where T : struct, Enum
	{
		if (Enum.TryParse<T>(value, out var result)) return result;
		return null;
	}

	public static IEnumerable<T> GetValues<T>()
		where T : struct, Enum =>
		Enum.GetValues<T>().OrderBy(x => x.GetOrder());

	public static int GetOrder<TEnum>(this TEnum e)
		where TEnum : struct, Enum, IConvertible
	{
		static int getValue(object obj) =>
			Convert.ToInt32(obj);

		var attribute = e.getEnumAttribute<DisplayAttribute>();
		if (attribute is null) return getValue(e);

		return attribute.GetOrder() ?? getValue(e);
	}

	public static IEnumerable<T> OrderByEnum<T, TEnum>(this IEnumerable<T> values, Func<T, TEnum> select)
		where TEnum : struct, Enum =>
		values.OrderBy(x => select(x).GetOrder());

	public static IEnumerable<T> OrderByEnumDesc<T, TEnum>(this IEnumerable<T> values, Func<T, TEnum> select)
		where TEnum : struct, Enum =>
		values.OrderByDescending(x => select(x).GetOrder());

	public static string GetDescription(this Enum value) =>
		StringExt.WhitespaceCoalesce(
			value.getEnumAttributeProperty<DisplayAttribute, string?>(x => x.Description),
			value.ToString()
		);

	public static string GetName(this Enum value) =>
		StringExt.WhitespaceCoalesce(
			value.getEnumAttributeProperty<DisplayAttribute, string?>(x => x.Name),
			value.ToString()
		);
	#endregion

	#region private methods
	private static TProperty? getEnumAttributeProperty<TAttribute, TProperty>(this Enum value, Func<TAttribute, TProperty?> property)
		where TAttribute : Attribute
	{
		var attribute = value.getEnumAttribute<TAttribute>();
		if (attribute is null) return default;

		return property(attribute);
	}

	private static T? getEnumAttribute<T>(this Enum value)
		where T : Attribute =>
		value
			.getEnumAttributes<T>()
			.FirstOrDefault();

	private static IReadOnlyCollection<T> getEnumAttributes<T>(this Enum value)
		where T : Attribute =>
		value
			.GetType()?
			.GetField(value.ToString())?.GetCustomAttributes(typeof(T), false) as T[] ??
			[];
	#endregion
}