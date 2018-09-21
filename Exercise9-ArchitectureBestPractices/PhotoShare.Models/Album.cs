namespace PhotoShare.Models
{
    using System.Collections.Generic;
    using PhotoShare.Models.Enums;

    public class Album
    {
	public Album()
	{
	    Pictures = new HashSet<Picture>();
	    AlbumTags = new HashSet<AlbumTag>();
	    AlbumRoles = new HashSet<AlbumRole>();
	}

	public int Id { get; set; }
	public string Name { get; set; }
	public Color? BackgroundColor { get; set; }
	public bool IsPublic { get; set; }
	public ICollection<AlbumRole> AlbumRoles { get; set; }
	public ICollection<Picture> Pictures { get; set; }
	public ICollection<AlbumTag> AlbumTags { get; set; }
    }
}
