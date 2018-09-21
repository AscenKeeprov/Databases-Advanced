using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Data.DataTransferObjects;
using TeamBuilder.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    public class InviteToTeamCommand : ICommand
    {

	private readonly IServiceProvider serviceProvider;
	private IAuthenticationManager AuthenticationManager
	    => serviceProvider.GetService<IAuthenticationManager>();
	private ITeamController TeamController
	    => serviceProvider.GetService<ITeamController>();
	private IUserController UserController
	    => serviceProvider.GetService<IUserController>();
	private IInvitationController InvitationController
	    => serviceProvider.GetService<IInvitationController>();

	public InviteToTeamCommand(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public string Execute(IList<string> commandArgs)
	{
	    if (!AuthenticationManager.IsAuthenticated())
		throw new InvalidOperationException(Messages.LoginFirst);
	    UserDto inviter = AuthenticationManager.GetCurrentUser();
	    if (commandArgs.Count != Constants.InviteToTeamCommandArgumentsCount)
		throw new FormatException(Messages.InvalidArgumentsCount);
	    string teamName = commandArgs[0];
	    TeamDto team = TeamController.GetTeam(teamName);
	    if (team == null) throw new ArgumentException(String.Format(Messages.TeamNotExist, teamName));
	    if (!TeamController.IsMember(team, inviter.Id)) throw new InvalidOperationException(
		String.Format(Messages.UserCannotSendInvite, inviter.Username, teamName));
	    string inviteeUsername = commandArgs[1];
	    UserDto invitee = UserController.GetUser(inviteeUsername);
	    if (invitee == null) throw new ArgumentException(
		String.Format(Messages.UserNotExist, inviteeUsername));
	    if (InvitationController.InvitationExists(team.Id, invitee.Id))
		throw new InvalidOperationException(String.Format(
		    Messages.InvitationExists, teamName, inviteeUsername));
	    if (TeamController.IsMember(team, invitee.Id)) throw new InvalidOperationException(
		String.Format(Messages.UserAlreadyMember, inviteeUsername, teamName));
	    InvitationController.AddInvitation(team.Id, invitee.Id);
	    return String.Format(Messages.InvitationSent, teamName, inviteeUsername);
	}
    }
}
