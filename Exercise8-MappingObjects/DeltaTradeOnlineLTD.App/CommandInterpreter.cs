using System;
using DeltaTradeOnlineLTD.App.Interfaces;
using DeltaTradeOnlineLTD.Models.Interfaces;
using DeltaTradeOnlineLTD.Services.Interfaces;

namespace DeltaTradeOnlineLTD.App
{
    public class CommandInterpreter : ICommandInterpreter
    {
	private const string CommandPrompt = "Enter a command to execute: ";

	private readonly ICommandFactory commandFactory;

	public CommandInterpreter(ICommandFactory commandFactory)
	{
	    this.commandFactory = commandFactory;
	}

	public void ProcessCommands()
	{
	    Console.Write(CommandPrompt);
	    while (true)
	    {
		string[] commandInput = Console.ReadLine()
		    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
		try
		{
		    ICommand command = commandFactory.CreateCommand(commandInput);
		    command.Execute();
		}
		catch (Exception exception)
		{
		    Console.WriteLine(exception.Message);
		}
		Console.Write(CommandPrompt);
	    }
	}
    }
}
