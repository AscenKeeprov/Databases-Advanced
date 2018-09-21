namespace PhotoShare.Client.Core.Exceptions
{
    using System;

    public class InvalidPermissionException : ArgumentException
    {
	private const string message = "Permission must be either “Owner” or “Viewer”!";

	public InvalidPermissionException() : base(message) { }
    }
}
