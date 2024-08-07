namespace TylerDM.StandardLibrary;

public static class Namespaces
{
	[Fact]
	public static void EnsureCorrectNamespacePrefix()
	{
		var classesWithIncorrectNamespace =
			from x in typeof(Startup).Assembly.GetTypes()
			let y = x.Namespace ?? ""
			where y.StartsWith("TylerDM.StandardLibrary") == false
			select x;
		if (classesWithIncorrectNamespace.Any())
			throw new Exception("Classes found with incorrect namespace.");
	}
}
