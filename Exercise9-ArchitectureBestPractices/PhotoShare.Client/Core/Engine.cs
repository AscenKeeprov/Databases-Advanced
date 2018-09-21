namespace PhotoShare.Client.Core
{
    using System;
    using System.Data.SqlClient;
    using Microsoft.Extensions.DependencyInjection;
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Services.Contracts;

    public class Engine : IEngine
    {
	private readonly IServiceProvider serviceProvider;

	public Engine(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public void Run()
	{
	    var initializeService = serviceProvider.GetService<IDatabaseInitializerService>();
	    initializeService.InitializeDatabase();
	    var commandInterpreter = serviceProvider.GetService<ICommandInterpreter>();
	    while (true)
	    {
		Console.Write("Enter command: ");
		try
		{
		    string[] input = Console.ReadLine()
			.Split(" ", StringSplitOptions.RemoveEmptyEntries);
		    string result = commandInterpreter.Read(input);
		    Console.WriteLine(result);
		}
		catch (Exception exception)
		when (exception is SqlException
		    || exception is ArgumentException
		    || exception is InvalidOperationException)
		{
		    Console.WriteLine(exception.Message);
		}
	    }
	}
    }
}
