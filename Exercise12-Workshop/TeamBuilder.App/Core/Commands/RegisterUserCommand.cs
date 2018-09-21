using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Models.Enumerations;
using TeamBuilder.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    public class RegisterUserCommand : ICommand
    {
	private readonly IServiceProvider serviceProvider;
	private IAuthenticationManager AuthenticationManager
	    => serviceProvider.GetService<IAuthenticationManager>();
	private IUserController UserController
	    => serviceProvider.GetService<IUserController>();

	public RegisterUserCommand(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public string Execute(IList<string> commandArgs)
	{
	    if (AuthenticationManager.IsAuthenticated())
		throw new InvalidOperationException(Messages.LogoutFirst);
	    if (commandArgs.Count != Constants.RegisterUserCommandArgumentsCount)
		throw new FormatException(Messages.InvalidArgumentsCount);
	    string username = commandArgs[0];
	    if (UserController.UserExists(username))
		throw new InvalidOperationException(String.Format(Messages.UsernameTaken, username));
	    string password = commandArgs[1];
	    string repeatPassword = commandArgs[2];
	    if (!repeatPassword.Equals(password))
		throw new InvalidOperationException(Messages.PasswordMismatch);
	    string firstName = commandArgs[3];
	    string lastName = commandArgs[4];
	    if (!int.TryParse(commandArgs[5], out int age))
		throw new ArgumentException(Messages.AgeNotValidNotNumber);
	    if (!Enum.TryParse(commandArgs[6], true, out Gender gender))
		throw new ArgumentException(Messages.GenderNotValid);
	    UserController.RegisterUser(username, password, firstName, lastName, age, gender);
	    return String.Format(Messages.UserRegistered, username);
	}
    }
}
