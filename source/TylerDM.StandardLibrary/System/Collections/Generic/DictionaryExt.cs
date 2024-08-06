namespace TylerDM.StandardLibrary.System.Collections.Generic;

public static class DictionaryExt
{
	public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> func)
		where TKey : notnull
	{
		if (dictionary.TryGetValue(key, out var outvar)) return outvar;
		var value = func(key);
		dictionary.Add(key, value);
		return value;
	}

	[Obsolete("Consider using ConcurrentDictionary or another thread safe collection instead.")]
	public static async Task<TValue> GetOrAddAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, Task<TValue>> funcAsync)
		where TKey : notnull
	{
		if (dictionary.TryGetValue(key, out var outvar)) return outvar;
		var value = await funcAsync(key);
		dictionary.Add(key, value);
		return value;
	}
}
