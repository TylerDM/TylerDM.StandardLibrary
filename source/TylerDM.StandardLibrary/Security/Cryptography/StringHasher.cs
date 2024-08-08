using System.Security.Cryptography;

namespace TylerDM.StandardLibrary.Security.Cryptography;

public static class StringHasher
{
	#region fields
	private static readonly SHA1 _sha1 = SHA1.Create();
	#endregion

	#region methods
	public static byte[] GetComputedHash(string password, byte[] salt)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(password, nameof(password));
		if (salt.Length is 0) throw new ArgumentOutOfRangeException(nameof(salt));

		var slurry = getSlurry(password, salt);
		return _sha1.ComputeHash(slurry);
	}

	public static bool GetHashMatches(string password, byte[] storedHash, byte[] salt)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(password, nameof(password));
		if (storedHash.Length is 0) throw new ArgumentOutOfRangeException(nameof(storedHash));
		if (salt.Length is 0) throw new ArgumentOutOfRangeException(nameof(salt));

		var computedHash = GetComputedHash(password, salt);
		return getIsEqual(storedHash, computedHash);
	}

	public static CreateHashResult CreateHashSaltPair(string password, int saltLength = 10)
	{
		var salt = RandomNumberGenerator.GetBytes(saltLength);
		var hash = GetComputedHash(password, salt);
		return new(hash, salt);
	}
	#endregion

	#region private methods
	private static bool getIsEqual(byte[] b1, byte[] b2)
	{
		if (b1.Length != b2.Length) return false;
		return b1.SequenceEqual(b2);
	}

	private static byte[] getSlurry(string password, byte[] salt)
	{
		var bytes = Encoding.UTF8.GetBytes(password);
		return [.. bytes, .. salt];
	}
	#endregion
}