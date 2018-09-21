namespace PhotoShare.Client.Core.Exceptions
{
    using System;

    public class InvalidObjectException : ArgumentException
    {
	private const string message = "Invalid {0}!";

	public InvalidObjectException(string objectName)
	    : base(String.Format(message, objectName)) { }
    }
}
