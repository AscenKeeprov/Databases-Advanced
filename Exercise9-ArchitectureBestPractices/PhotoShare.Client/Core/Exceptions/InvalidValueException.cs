namespace PhotoShare.Client.Core.Exceptions
{
    using System;

    public class InvalidValueException : ArgumentException
    {
	private const string message = "Value {0} not valid.{1}";

	public InvalidValueException(string value, string detailedMessage)
	    : base(String.Format(message, value, detailedMessage)) { }
    }
}
