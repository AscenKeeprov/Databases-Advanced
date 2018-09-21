using System;
using System.Collections.Generic;
using TeamBuilder.App.Core.Interfaces;

namespace TeamBuilder.App.Core.Commands
{
    public class AddTeamToCommand : ICommand
    {
	private readonly IServiceProvider serviceProvider;

	public AddTeamToCommand(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public string Execute(IList<string> commandArgs)
	{
	    throw new System.NotImplementedException();
	}
    }
}
