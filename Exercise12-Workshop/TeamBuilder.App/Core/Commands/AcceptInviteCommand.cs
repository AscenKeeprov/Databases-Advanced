﻿using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Data.DataTransferObjects;
using TeamBuilder.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    public class AcceptInviteCommand : ICommand
    {
	private readonly IServiceProvider serviceProvider;
	private IAuthenticationManager AuthenticationManager
	    => serviceProvider.GetService<IAuthenticationManager>();
	private ITeamController TeamController
	    => serviceProvider.GetService<ITeamController>();
	private IInvitationController InvitationController
	    => serviceProvider.GetService<IInvitationController>();

	public AcceptInviteCommand(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public string Execute(IList<string> commandArgs)
	{
	    if (!AuthenticationManager.IsAuthenticated())
		throw new InvalidOperationException(Messages.LoginFirst);
	    UserDto invitee = AuthenticationManager.GetCurrentUser();
	    if (commandArgs.Count != Constants.AcceptInviteCommandArgumentsCount)
		throw new FormatException(Messages.InvalidArgumentsCount);
	    string teamName = commandArgs[0];
	    TeamDto team = TeamController.GetTeam(teamName);
	    if (team == null) throw new ArgumentException(String.Format(Messages.TeamNotExist, teamName));
	    if (!InvitationController.InvitationExists(team.Id, invitee.Id))
		throw new ArgumentException(String.Format(
		    Messages.InvitationNotExist, teamName, invitee.Username));
	    TeamController.AddMember(team.Id, invitee.Id);
	    InvitationController.RemoveInvitation(team.Id, invitee.Id);
	    return String.Format(Messages.InvitationAccepted, invitee.Username, teamName);
	}
    }
}
