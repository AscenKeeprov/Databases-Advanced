using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Data.DataTransferObjects;
using TeamBuilder.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    public class DisbandCommand : ICommand
    {
	private readonly IServiceProvider serviceProvider;
	private IAuthenticationManager AuthenticationManager
	    => serviceProvider.GetService<IAuthenticationManager>();
	private ITeamController TeamController
	    => serviceProvider.GetService<ITeamController>();

	public DisbandCommand(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public string Execute(IList<string> commandArgs)
	{
	    if (!AuthenticationManager.IsAuthenticated())
		throw new InvalidOperationException(Messages.LoginFirst);
	    UserDto currentUser = AuthenticationManager.GetCurrentUser();
	    if (commandArgs.Count != Constants.DisbandCommandArgumentsCount)
		throw new FormatException(Messages.InvalidArgumentsCount);
	    string teamName = commandArgs[0];
	    TeamDto team = TeamController.GetTeam(teamName);
	    if (team == null) throw new ArgumentException(String.Format(Messages.TeamNotExist, teamName));
	    if (!TeamController.IsCreator(team, currentUser.Id))
		throw new InvalidOperationException(String.Format(
		    Messages.UserCannotDisbandTeam, currentUser.Username, teamName));
	    TeamController.DisbandTeam(team.Id);
	    return String.Format(Messages.TeamDisbanded, teamName);
	}
    }
}
