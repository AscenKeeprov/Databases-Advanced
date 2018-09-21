namespace PhotoShare.Client.Core.Dtos
{
    using PhotoShare.Client.Core.Validation;

    public class RegisterUserDto
    {
	public string Username { get; set; }

	[Password(4, 20)]
	public string Password { get; set; }

	[Email]
	public string Email { get; set; }
    }
}
