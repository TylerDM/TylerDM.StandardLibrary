using System.Security.Cryptography;

namespace TylerDM.StandardLibrary.Security.Cryptography;

public class StringHasher(int _saltLength = 10)
{
	#region fields
	private static readonly SHA1 _sha1 = SHA1.Create();
	#endregion

	#region properties
	public static StringHasher Default { get; } = new();
	#endregion

	#region methods
	public byte[] GetComputedHash(string password, byte[] salt)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(password, nameof(password));
		if (salt.Length is 0) throw new ArgumentOutOfRangeException(nameof(salt));

		var slurry = getSlurry(password, salt);
		return _sha1.ComputeHash(slurry);
	}

	public bool GetHashMatches(string password, byte[] storedHash, byte[] salt)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(password, nameof(password));
		if (storedHash.Length is 0) throw new ArgumentOutOfRangeException(nameof(storedHash));
		if (salt.Length is 0) throw new ArgumentOutOfRangeException(nameof(salt));

		var computedHash = GetComputedHash(password, salt);
		return getIsEqual(storedHash, computedHash);
	}

	public CreateHashResult CreateHashSaltPair(string password)
	{
		var salt = RandomNumberGenerator.GetBytes(_saltLength);
		var hash = GetComputedHash(password, salt);
		return new(hash, salt);
	}
	#endregion

	#region private methods
	private bool getIsEqual(byte[] b1, byte[] b2)
	{
		if (b1.Length != b2.Length) return false;
		return b1.SequenceEqual(b2);
	}

	private byte[] getSlurry(string password, byte[] salt)
	{
		var bytes = Encoding.UTF8.GetBytes(password);
		return [.. bytes, .. salt];
	}
	#endregion
}