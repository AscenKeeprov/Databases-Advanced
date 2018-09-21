namespace PhotoShare.Client.Core.Dtos
{
    using System.Collections.Generic;

    public class AlbumTagsDto
    {
	public int Id { get; set; }
	public string Name { get; set; }
	public ICollection<AlbumTagDto> Tags { get; set; }
    }
}
