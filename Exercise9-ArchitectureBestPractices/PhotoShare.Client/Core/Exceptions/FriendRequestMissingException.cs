namespace PhotoShare.Client.Core.Exceptions
{
    using System;

    public class FriendRequestMissingException : InvalidOperationException
    {
	private const string message = "{0} has not added {1} as a friend!";

	public FriendRequestMissingException(string friendName, string userName)
	    : base(String.Format(message, friendName, userName)) { }
    }
}
