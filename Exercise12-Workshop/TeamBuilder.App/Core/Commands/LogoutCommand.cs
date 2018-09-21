using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Data.DataTransferObjects;
using TeamBuilder.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    public class LogoutCommand : ICommand
    {
	private readonly IServiceProvider serviceProvider;
	private IAuthenticationManager AuthenticationManager
	    => serviceProvider.GetService<IAuthenticationManager>();

	public LogoutCommand(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public string Execute(IList<string> commandArgs)
	{
	    if (commandArgs.Count != Constants.LogoutCommandArgumentsCount)
		throw new FormatException(Messages.InvalidArgumentsCount);
	    UserDto currentUser = AuthenticationManager.GetCurrentUser();
	    AuthenticationManager.Logout();
	    return String.Format(Messages.UserLoggedOut, currentUser.Username);
	}
    }
}
