using System;
using System.Collections.Generic;
using TeamBuilder.App.Core.Interfaces;

namespace TeamBuilder.App.Core.Commands
{
    public class ShowEventCommand : ICommand
    {
	private readonly IServiceProvider serviceProvider;

	public ShowEventCommand(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public string Execute(IList<string> commandArgs)
	{
	    throw new System.NotImplementedException();
	}
    }
}
