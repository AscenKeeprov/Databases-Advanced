namespace PhotoShare.Client.Core.Exceptions
{
    using System;

    public class PermissionAlreadyGrantedException : InvalidOperationException
    {
	private const string message = "User {0} already has {1} permission for album {2}!";

	public PermissionAlreadyGrantedException(string username, string permission, string albumName)
	    : base(String.Format(message, username, permission, albumName)) { }
    }
}
