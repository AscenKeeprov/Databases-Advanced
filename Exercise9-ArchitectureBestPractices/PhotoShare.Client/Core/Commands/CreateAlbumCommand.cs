namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Client.Core.Exceptions;
    using PhotoShare.Client.Utilities;
    using PhotoShare.Models;
    using PhotoShare.Models.Enums;
    using PhotoShare.Services.Contracts;

    public class CreateAlbumCommand : ICommand
    {
	private const string SuccessMessage = "Album {0} successfully created!";

	private readonly IAlbumService albumService;
	private readonly IUserService userService;
	private readonly ITagService tagService;

	public CreateAlbumCommand(IAlbumService albumService, IUserService userService, ITagService tagService)
	{
	    this.albumService = albumService;
	    this.userService = userService;
	    this.tagService = tagService;
	}

	// CreateAlbum <username> <albumName> <backgroundColor> <tagName1> ... <tagNameN>
	public string Execute(string[] data)
	{
	    string username = data[0];
	    UserDto userDTO = userService.ByUsername<UserDto>(username);
	    if (userDTO == null || userDTO.IsDeleted == true)
		throw new ObjectNotFoundException(typeof(User).Name, username);
	    string albumName = data[1];
	    if (albumService.Exists(albumName))
		throw new DuplicateObjectException(typeof(Album).Name, albumName);
	    string colorName = data[2];
	    if (!Enum.TryParse(colorName, true, out Color backgroundColor))
		throw new ObjectNotFoundException(typeof(Color).Name, colorName);
	    string[] tagNames = data.Skip(3).Select(t => t.ValidateOrTransform()).ToArray();
	    if (tagNames.Any(tagName => !tagService.Exists(tagName)))
		throw new InvalidObjectException("tags");
	    albumService.Create(userDTO.Id, albumName, backgroundColor.ToString(), tagNames);
	    return String.Format(SuccessMessage, albumName);
	}
    }
}
