using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Data.DataTransferObjects;
using TeamBuilder.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    public class DeleteUserCommand : ICommand
    {
	private readonly IServiceProvider serviceProvider;
	private IAuthenticationManager AuthenticationManager
	    => serviceProvider.GetService<IAuthenticationManager>();
	private IUserController UserController
	    => serviceProvider.GetService<IUserController>();

	public DeleteUserCommand(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public string Execute(IList<string> commandArgs)
	{
	    if (commandArgs.Count != Constants.DeleteUserCommandArgumentsCount)
		throw new FormatException(Messages.InvalidArgumentsCount);
	    if (!AuthenticationManager.IsAuthenticated())
		throw new InvalidOperationException(Messages.LoginFirst);
	    UserDto currentUser = AuthenticationManager.GetCurrentUser();
	    UserController.DeleteUser(currentUser.Id);
	    AuthenticationManager.Logout();
	    return String.Format(Messages.UserDeleted, currentUser.Username);
	}
    }
}
