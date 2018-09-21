namespace PhotoShare.Client.Core.Commands
{
    using System;
    using PhotoShare.Client.Core.Contracts;

    public class ExitCommand : ICommand
    {
	public string Execute(string[] data)
	{
	    Console.WriteLine("Have a nice day!");
	    Environment.Exit(0);
	    return null;
	}
    }
}
