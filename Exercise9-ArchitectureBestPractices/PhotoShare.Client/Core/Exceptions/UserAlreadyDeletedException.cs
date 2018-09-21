namespace PhotoShare.Client.Core.Exceptions
{
    using System;

    public class UserAlreadyDeletedException : InvalidOperationException
    {
	private const string message = "User {0} has already been deleted!";

	public UserAlreadyDeletedException(string username)
	    : base(String.Format(message, username)) { }
    }
}
