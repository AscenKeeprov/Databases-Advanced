namespace PhotoShare.Client.Core.Commands
{
    using System;
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Client.Core.Exceptions;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class DeleteUserCommand : ICommand
    {
	private const string SuccessMessage = "User {0} was deleted successfully!";

	private readonly IUserService userService;

	public DeleteUserCommand(IUserService userService)
	{
	    this.userService = userService;
	}

	// DeleteUser <username>
	public string Execute(string[] data)
	{
	    string username = data[0];
	    UserDto userDTO = userService.ByUsername<UserDto>(username);
	    if (userDTO == null) throw new ObjectNotFoundException(typeof(User).Name, username);
	    if (userDTO.IsDeleted == true) throw new UserAlreadyDeletedException(username);
	    userService.Delete(username);
	    return String.Format(SuccessMessage, username);
	}
    }
}
