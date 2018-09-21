namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Client.Core.Exceptions;
    using PhotoShare.Models;
    using PhotoShare.Models.Enums;
    using PhotoShare.Services.Contracts;

    public class ShareAlbumCommand : ICommand
    {
	private const string SuccessMessage = "User {0} added to album {1} ({2})";

	private readonly IAlbumService albumService;
	private readonly IAlbumRoleService albumRoleService;
	private readonly IUserService userService;

	public ShareAlbumCommand(IAlbumService albumService, IAlbumRoleService albumRoleService, IUserService userService)
	{
	    this.albumService = albumService;
	    this.albumRoleService = albumRoleService;
	    this.userService = userService;
	}

	// ShareAlbum <albumId> <username> <permission>
	public string Execute(string[] data)
	{
	    int albumId = int.Parse(data[0]);
	    AlbumDto albumDTO = albumService.ById<AlbumDto>(albumId);
	    if (albumDTO == null) throw new ObjectNotFoundException(typeof(Album).Name, data[0]);
	    string username = data[1];
	    UserRolesDto userDTO = userService.ByUsername<UserRolesDto>(username);
	    if (userDTO == null || userDTO.IsDeleted == true)
		throw new ObjectNotFoundException(typeof(User).Name, username);
	    if (!Enum.TryParse(data[2], true, out Role role))
		throw new InvalidPermissionException();
	    string permission = role.ToString();
	    AlbumRoleDto alreadySharedAlbum = userDTO.Permissions
		.FirstOrDefault(p => p.AlbumName == albumDTO.Name);
	    if (alreadySharedAlbum != null && alreadySharedAlbum.Permission == permission)
		throw new PermissionAlreadyGrantedException(username, permission, albumDTO.Name);
	    AlbumRole albumRole = albumRoleService.PublishAlbumRole(albumId, userDTO.Id, permission);
	    return String.Format(SuccessMessage, username, albumDTO.Name, permission);
	}
    }
}
