namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Client.Core.Exceptions;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class RegisterUserCommand : ICommand
    {
	private const string SuccessMessage = "User {0} was registered successfully!";

	private readonly IUserService userService;

	public RegisterUserCommand(IUserService userService)
	{
	    this.userService = userService;
	}

	// RegisterUser <username> <password> <repeat-password> <email>
	public string Execute(string[] data)
	{
	    string username = data[0];
	    if (userService.Exists(username))
		throw new DuplicateObjectException(typeof(User).Name, username);
	    string password = data[1];
	    string repeatPassword = data[2];
	    if (repeatPassword != password) throw new PasswordMismatchException();
	    string email = data[3];
	    RegisterUserDto userDTO = new RegisterUserDto()
	    {
		Username = username,
		Password = password,
		Email = email
	    };
	    if (!IsValid(userDTO)) throw new InvalidObjectException("registration data");
	    User user = userService.Register(username, password, email);
	    return String.Format(SuccessMessage, username);
	}

	private bool IsValid(object obj)
	{
	    var validationContext = new ValidationContext(obj);
	    var validationResults = new List<ValidationResult>();
	    return Validator.TryValidateObject(obj, validationContext, validationResults, true);
	}
    }
}
