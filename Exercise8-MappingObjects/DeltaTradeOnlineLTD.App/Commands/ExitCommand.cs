using System;
using System.Threading;
using DeltaTradeOnlineLTD.Models;

namespace DeltaTradeOnlineLTD.App.Commands
{
    public class ExitCommand : Command
    {
	protected override int MaxAllowedParameters => 0;

	public ExitCommand(params string[] parameters) : base(parameters) { }

	public override void Execute()
	{
	    Console.Write("Disconnecting from database ");
	    for (int second = 1; second <= 3; second++)
	    {
		Thread.Sleep(768);
		Console.Write(".");
	    }
	    Console.WriteLine($"{Environment.NewLine}Have a nice day!");
	    Environment.Exit(0);
	}
    }
}
