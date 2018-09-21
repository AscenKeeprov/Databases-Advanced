namespace TeamBuilder.Data.DataTransferObjects
{
    public class UserDto
    {
	public int Id { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
	public bool IsDeleted { get; set; }
    }
}
