namespace PhotoShare.Client.Core.Commands
{
    using System;
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Client.Core.Exceptions;
    using PhotoShare.Client.Utilities;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class AddTagCommand : ICommand
    {
	private const string SuccessMessage = "Tag {0} was added successfully!";

	private readonly ITagService tagService;

	public AddTagCommand(ITagService tagService)
	{
	    this.tagService = tagService;
	}

	// AddTag <tagName>
	public string Execute(string[] data)
	{
	    string tagName = data[0];
	    if (tagService.Exists(tagName))
		throw new DuplicateObjectException(typeof(Tag).Name, tagName);
	    tagName = tagName.ValidateOrTransform();
	    Tag tag = tagService.AddTag(tagName);
	    return String.Format(SuccessMessage, tagName);
	}
    }
}
