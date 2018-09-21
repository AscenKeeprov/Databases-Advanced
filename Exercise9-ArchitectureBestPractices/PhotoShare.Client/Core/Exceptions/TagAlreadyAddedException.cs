namespace PhotoShare.Client.Core.Exceptions
{
    using System;

    public class TagAlreadyAddedException : InvalidOperationException
    {
	private const string message = "Tag {0} has already been added to album {1}!";

	public TagAlreadyAddedException(string tagName, string albumName)
	    : base(String.Format(message, tagName, albumName)) { }
    }
}
