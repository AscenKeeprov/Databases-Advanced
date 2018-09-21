namespace PhotoShare.Client.Core.Exceptions
{
    using System;

    public class PasswordMismatchException : ArgumentException
    {
	private const string message = "Passwords do not match!";

	public PasswordMismatchException() : base(message) { }
    }
}
