namespace PhotoShare.Client.Core.Exceptions
{
    using System;

    public class FriendRequestExistsException : InvalidOperationException
    {
	private const string message = "{0} has already sent a friendship request to {1}!";

	public FriendRequestExistsException(string userName, string friendName)
	    : base(String.Format(message, userName, friendName)) { }
    }
}
