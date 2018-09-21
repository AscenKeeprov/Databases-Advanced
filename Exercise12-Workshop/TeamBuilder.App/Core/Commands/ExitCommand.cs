using System;
using System.Collections.Generic;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    public class ExitCommand : ICommand
    {
	public string Execute(IList<string> commandArgs)
	{
	    if (commandArgs.Count != Constants.ExitCommandArgumentsCount)
	    {
		throw new FormatException(Messages.InvalidArgumentsCount);
	    }
	    Console.WriteLine("Have a nice day!");
	    Environment.Exit(0);
	    return null;
	}
    }
}
