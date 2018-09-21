namespace PhotoShare.Client.Core.Exceptions
{
    using System;

    public class ObjectNotFoundException : ArgumentException
    {
	private const string message = "{0} {1} not found!";

	public ObjectNotFoundException(string objectType, string objectName)
	    : base(String.Format(message, objectType, objectName)) { }
    }
}
