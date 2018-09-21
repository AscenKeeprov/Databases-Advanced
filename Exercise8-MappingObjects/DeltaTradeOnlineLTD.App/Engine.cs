using System;
using DeltaTradeOnlineLTD.App.Interfaces;
using DeltaTradeOnlineLTD.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DeltaTradeOnlineLTD.App
{
    public class Engine : IEngine
    {
	private readonly IDbInitializer dbInitializer;
	private readonly ICommandInterpreter commandInterpreter;

	public Engine(IServiceProvider server)
	{
	    dbInitializer = server.GetService<IDbInitializer>();
	    commandInterpreter = server.GetService<ICommandInterpreter>();
	}

	public void Start()
	{
	    dbInitializer.InitializeDatabase();
	    commandInterpreter.ProcessCommands();
	}
    }
}
