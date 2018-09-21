namespace PhotoShare.Client.Core.Exceptions
{
    using System;

    public class UserHasNoFriendsException : InvalidOperationException
    {
	private const string message = "User {0} has no friends :(";

	public UserHasNoFriendsException(string username)
	    : base(String.Format(message, username)) { }
    }
}
