using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Data.DataTransferObjects;
using TeamBuilder.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    public class ShowTeamCommand : ICommand
    {
	private readonly IServiceProvider serviceProvider;
	private ITeamController TeamController
	    => serviceProvider.GetService<ITeamController>();

	public ShowTeamCommand(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public string Execute(IList<string> commandArgs)
	{
	    if (commandArgs.Count != Constants.ShowTeamCommandArgumentsCount)
		throw new FormatException(Messages.InvalidArgumentsCount);
	    string teamName = commandArgs[0];
	    TeamDto team = TeamController.GetTeam(teamName);
	    if (team == null) throw new ArgumentException(String.Format(Messages.TeamNotExist, teamName));
	    string teamInfo = TeamController.GetTeamInfo(team);
	    return teamInfo;
	}
    }
}
