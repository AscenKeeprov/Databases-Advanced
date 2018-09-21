namespace PhotoShare.Client.Core.Exceptions
{
    using System;

    public class PropertyNotSupportedException : ArgumentException
    {
	private const string message = "Property {0} not supported!";

	public PropertyNotSupportedException(string propertyName)
	    : base(String.Format(message, propertyName)) { }
    }
}
