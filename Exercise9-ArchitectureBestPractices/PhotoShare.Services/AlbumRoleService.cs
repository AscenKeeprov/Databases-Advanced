namespace PhotoShare.Services
{
    using System;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using PhotoShare.Models.Enums;
    using PhotoShare.Services.Contracts;

    public class AlbumRoleService : IAlbumRoleService
    {
	private readonly PhotoShareContext context;

	public AlbumRoleService(PhotoShareContext context)
	{
	    this.context = context;
	}

	public AlbumRole PublishAlbumRole(int albumId, int userId, string role)
	{
	    AlbumRole albumRole = new AlbumRole()
	    {
		AlbumId = albumId,
		UserId = userId,
		Role = Enum.Parse<Role>(role, true)
	    };
	    context.AlbumRoles.Add(albumRole);
	    context.SaveChanges();
	    return albumRole;
	}
    }
}
