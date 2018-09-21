using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Data.DataTransferObjects;
using TeamBuilder.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    public class LoginCommand : ICommand
    {
	private readonly IServiceProvider serviceProvider;
	private IAuthenticationManager AuthenticationManager
	    => serviceProvider.GetService<IAuthenticationManager>();
	private IUserController UserController
	    => serviceProvider.GetService<IUserController>();

	public LoginCommand(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public string Execute(IList<string> commandArgs)
	{
	    if (commandArgs.Count != Constants.LoginCommandArgumentsCount)
		throw new FormatException(Messages.InvalidArgumentsCount);
	    string username = commandArgs[0];
	    UserDto user = UserController.GetUser(username);
	    if (user == null) throw new ArgumentException(String.Format(Messages.UserNotExist, username));
	    string password = commandArgs[1];
	    if (!password.Equals(user.Password)) throw new ArgumentException(Messages.IncorrectPassword);
	    AuthenticationManager.Login(user);
	    return String.Format(Messages.UserLoggedIn, username);
	}
    }
}
