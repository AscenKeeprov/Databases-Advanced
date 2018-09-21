namespace PhotoShare.Client.Core.Exceptions
{
    using System;

    public class DuplicateObjectException : ArgumentException
    {
	private const string message = "{0} {1} already exists!";

	public DuplicateObjectException(string objectType, string objectName)
	    : base(String.Format(message, objectType, objectName)) { }
    }
}
