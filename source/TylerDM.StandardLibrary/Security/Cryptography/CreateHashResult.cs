namespace TylerDM.StandardLibrary.Security.Cryptography;

public record CreateHashResult(
	byte[] Hash,
	byte[] Salt
);
