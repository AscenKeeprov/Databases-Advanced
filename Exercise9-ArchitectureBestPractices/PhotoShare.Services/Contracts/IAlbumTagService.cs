namespace PhotoShare.Services.Contracts
{
    using PhotoShare.Models;

    public interface IAlbumTagService
    {
	AlbumTag AddTagTo(int albumId, int tagId);
    }
}
