namespace PhotoShare.Client.Core.Dtos
{
    using PhotoShare.Client.Core.Validation;
    using PhotoShare.Models;

    public class ModifyUserDto
    {
	public int Id { get; private set; }
	public Town BornTown { get; set; }
	public Town CurrentTown { get; set; }
	public bool? IsDeleted { get; set; }

	[Password(4, 20)]
	public string Password { get; set; }
    }
}
