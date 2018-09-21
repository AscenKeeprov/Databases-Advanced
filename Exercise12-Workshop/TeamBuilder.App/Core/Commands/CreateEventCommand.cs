using System;
using System.Collections.Generic;
using TeamBuilder.App.Core.Interfaces;

namespace TeamBuilder.App.Core.Commands
{
    public class CreateEventCommand : ICommand
    {
	private readonly IServiceProvider serviceProvider;

	public CreateEventCommand(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public string Execute(IList<string> commandArgs)
	{
	    throw new System.NotImplementedException();
	}
    }
}
