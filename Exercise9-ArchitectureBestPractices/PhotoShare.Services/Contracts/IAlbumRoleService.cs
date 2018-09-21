namespace PhotoShare.Services.Contracts
{
    using PhotoShare.Models;

    public interface IAlbumRoleService
    {
	AlbumRole PublishAlbumRole(int albumId, int userId, string role);
    }
}
