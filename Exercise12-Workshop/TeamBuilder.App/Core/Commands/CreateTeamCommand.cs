using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Data.DataTransferObjects;
using TeamBuilder.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    public class CreateTeamCommand : ICommand
    {
	private readonly IServiceProvider serviceProvider;
	private IAuthenticationManager AuthenticationManager
	    => serviceProvider.GetService<IAuthenticationManager>();
	private ITeamController TeamController
	    => serviceProvider.GetService<ITeamController>();

	public CreateTeamCommand(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public string Execute(IList<string> commandArgs)
	{
	    if (!AuthenticationManager.IsAuthenticated())
		throw new InvalidOperationException(Messages.LoginFirst);
	    UserDto teamCreator = AuthenticationManager.GetCurrentUser();
	    if (commandArgs.Count < Constants.CreateTeamCommandArgumentsCount)
		throw new FormatException(Messages.InvalidArgumentsCount);
	    string teamName = commandArgs[0];
	    if (TeamController.TeamExists(teamName))
		throw new ArgumentException(String.Format(Messages.TeamExists, teamName));
	    string acronym = commandArgs[1];
	    if (TeamController.AcronymInUse(acronym))
		throw new ArgumentException(String.Format(Messages.AcronymTaken, acronym));
	    string teamDescription = String.Join(" ", commandArgs.Skip(2));
	    TeamController.CreateTeam(teamName, acronym, teamDescription, teamCreator);
	    return String.Format(Messages.TeamCreated, teamName);
	}
    }
}
