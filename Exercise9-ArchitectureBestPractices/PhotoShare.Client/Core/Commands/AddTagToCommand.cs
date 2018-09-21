namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Client.Core.Exceptions;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class AddTagToCommand : ICommand
    {
	private const string SuccessMessage = "Tag {0} added to album {1}.";

	private readonly IAlbumService albumService;
	private readonly IAlbumTagService albumTagService;
	private readonly ITagService tagService;

	public AddTagToCommand(IAlbumService albumService, IAlbumTagService albumTagService, ITagService tagService)
	{
	    this.albumService = albumService;
	    this.albumTagService = albumTagService;
	    this.tagService = tagService;
	}

	// AddTagTo <albumName> <tag>
	public string Execute(string[] data)
	{
	    string albumName = data[0];
	    AlbumTagsDto albumDTO = albumService.ByName<AlbumTagsDto>(albumName);
	    if (albumDTO == null) throw new ObjectNotFoundException(typeof(Album).Name, albumName);
	    string tagName = $"#{data[1]}";
	    TagDto tagDTO = tagService.ByName<TagDto>(tagName);
	    if (tagDTO == null) throw new ObjectNotFoundException(typeof(Tag).Name, tagName);
	    if (albumDTO.Tags.Any(t => t.TagName == tagName))
		throw new TagAlreadyAddedException(tagName, albumName);
	    AlbumTag albumTag = albumTagService.AddTagTo(albumDTO.Id, tagDTO.Id);
	    return String.Format(SuccessMessage, tagName, albumName);
	}
    }
}
