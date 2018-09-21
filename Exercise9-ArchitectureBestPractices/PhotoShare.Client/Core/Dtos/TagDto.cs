namespace PhotoShare.Client.Core.Dtos
{
    using PhotoShare.Client.Core.Validation;

    public class TagDto
    {
	public int Id { get; set; }

	[Tag]
	public string Name { get; set; }
    }
}
