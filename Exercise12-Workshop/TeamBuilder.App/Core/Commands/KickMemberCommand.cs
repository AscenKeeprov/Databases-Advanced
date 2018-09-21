using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Data.DataTransferObjects;
using TeamBuilder.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    public class KickMemberCommand : ICommand
    {
	private readonly IServiceProvider serviceProvider;
	private IAuthenticationManager AuthenticationManager
	    => serviceProvider.GetService<IAuthenticationManager>();
	private ITeamController TeamController
	    => serviceProvider.GetService<ITeamController>();
	private IUserController UserController
	    => serviceProvider.GetService<IUserController>();

	public KickMemberCommand(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public string Execute(IList<string> commandArgs)
	{
	    if (!AuthenticationManager.IsAuthenticated())
		throw new InvalidOperationException(Messages.LoginFirst);
	    UserDto currentUser = AuthenticationManager.GetCurrentUser();
	    if (commandArgs.Count != Constants.KickMemberCommandArgumentsCount)
		throw new FormatException(Messages.InvalidArgumentsCount);
	    string teamName = commandArgs[0];
	    TeamDto team = TeamController.GetTeam(teamName);
	    if (team == null) throw new ArgumentException(String.Format(Messages.TeamNotExist, teamName));
	    if (!TeamController.IsCreator(team, currentUser.Id)) throw new InvalidOperationException(
		String.Format(Messages.UserCannotKickMembers, currentUser.Username, teamName));
	    string targetUsername = commandArgs[1];
	    UserDto targetUser = UserController.GetUser(targetUsername);
	    if (targetUser == null) throw new ArgumentException(
		String.Format(Messages.UserNotExist, targetUsername));
	    if (TeamController.IsCreator(team, targetUser.Id)) throw new InvalidOperationException(
		String.Format(Messages.UserCannotKickSelf, targetUsername, teamName));
	    TeamController.RemoveMember(team.Id, targetUser.Id);
	    return String.Format(Messages.TeamMemberKicked, targetUsername, teamName);
	}
    }
}
