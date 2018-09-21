namespace PhotoShare.Models
{
    using System.Collections.Generic;

    public class Tag
    {
	public Tag()
	{
	    AlbumTags = new HashSet<AlbumTag>();
	}

	public int Id { get; set; }
	public string Name { get; set; }

	public ICollection<AlbumTag> AlbumTags { get; set; }
    }
}
