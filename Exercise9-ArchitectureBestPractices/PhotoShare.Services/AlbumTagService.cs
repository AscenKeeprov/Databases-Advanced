namespace PhotoShare.Services
{
    using System.Linq;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class AlbumTagService : IAlbumTagService
    {
	private readonly PhotoShareContext context;

	public AlbumTagService(PhotoShareContext context)
	{
	    this.context = context;
	}

	public AlbumTag AddTagTo(int albumId, int tagId)
	{
	    AlbumTag albumTag = new AlbumTag()
	    {
		AlbumId = albumId,
		TagId = tagId
	    };
	    if (!context.AlbumTags.Any(at => at.AlbumId == albumId && at.TagId == tagId))
		context.AlbumTags.Add(albumTag);
	    context.SaveChanges();
	    return albumTag;
	}
    }
}
