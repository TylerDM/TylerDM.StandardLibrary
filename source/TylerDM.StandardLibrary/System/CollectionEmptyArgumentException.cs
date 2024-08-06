namespace TylerDM.StandardLibrary.System;

public class CollectionEmptyArgumentException : ArgumentException
{
	private const string _message = "Collection cannot be empty is empty.";

	public string ParameterName { get; }

	public CollectionEmptyArgumentException(string parameterName) : base(_message, parameterName)
	{
		ThrowIfNullOrWhiteSpace(parameterName, nameof(parameterName));

		ParameterName = parameterName;
	}
}