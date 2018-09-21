namespace PhotoShare.Client.Core.Dtos
{
    using System.Collections.Generic;

    public class UserRolesDto
    {
	public int Id { get; set; }
	public string Username { get; set; }
	public bool? IsDeleted { get; set; }
	public ICollection<AlbumRoleDto> Permissions { get; set; }
    }
}
