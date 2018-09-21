namespace PhotoShare.Client.Core.Exceptions
{
    using System;

    public class UsersAlreadyFriendsException : InvalidOperationException
    {
	private const string message = "{0} is already a friend to {1}";

	public UsersAlreadyFriendsException(string friendName, string userName)
	    : base(String.Format(message, friendName, userName)) { }
    }
}
