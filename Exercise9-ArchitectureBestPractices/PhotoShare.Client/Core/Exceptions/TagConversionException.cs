namespace PhotoShare.Client.Core.Exceptions
{
    using System;

    public class TagConversionException : InvalidOperationException
    {
	private const string message = "Cannot convert empty string to a valid tag!";

	public TagConversionException() : base(message) { }
    }
}
