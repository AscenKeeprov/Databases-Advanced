using System;
using AutoMapper;
using DeltaTradeOnlineLTD.App;
using DeltaTradeOnlineLTD.App.Controllers;
using DeltaTradeOnlineLTD.App.Interfaces;
using DeltaTradeOnlineLTD.Data;
using DeltaTradeOnlineLTD.Services;
using DeltaTradeOnlineLTD.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeltaTradeOnlineLTD
{
    public class StartUp
    {
	public static void Main()
	{
	    IServiceProvider server = InitializeServices();
	    IEngine engine = new Engine(server);
	    engine.Start();
	}

	private static IServiceProvider InitializeServices()
	{
	    IServiceCollection services = new ServiceCollection();
	    services.AddDbContext<DeltaTradeOnlineDbContext>(optionsBuilder
		=> optionsBuilder.UseSqlServer(DbConfiguration.ConnectionString));
	    services.AddAutoMapper(configuration
		=> configuration.AddProfile<DeltaTradeOnlineProfile>());
	    services.AddTransient<IDbInitializer, DbInitializer>();
	    services.AddSingleton<ICommandFactory, CommandFactory>();
	    services.AddSingleton<ICommandInterpreter, CommandInterpreter>();
	    services.AddSingleton<IEmployeeController, EmployeeController>();
	    IServiceProvider serviceProvider = services.BuildServiceProvider();
	    return serviceProvider;
	}
    }
}
