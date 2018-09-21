using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TeamBuilder.App.Core.Controllers;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Data;

namespace TeamBuilder.App.Core
{
    public class Server
    {
	private readonly IServiceCollection services;
	private readonly IServiceProvider serviceProvider;
	private IDatabaseInitializer DbInitializer => serviceProvider.GetService<IDatabaseInitializer>();
	private ICommandDispatcher CommandDispatcher => serviceProvider.GetService<ICommandDispatcher>();

	public Server()
	{
	    services = new ServiceCollection();
	    serviceProvider = ConfigureServices(services);
	}

	private IServiceProvider ConfigureServices(IServiceCollection services)
	{
	    services.AddDbContext<TeamBuilderDbContext>(options
		 => options.UseLazyLoadingProxies(true)
		 .UseSqlServer(TeamBuilderDbConfiguration.ConnectionString));
	    services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
	    services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
	    services.AddSingleton<IAuthenticationManager, AuthenticationManager>();
	    services.AddTransient<IUserController, UserController>();
	    services.AddTransient<ITeamController, TeamController>();
	    services.AddTransient<IInvitationController, InvitationController>();
	    IServiceProvider serviceProvider = services.BuildServiceProvider();
	    return serviceProvider;
	}

	public void Run()
	{
	    DbInitializer.InitializeDatabase();
	    while (true)
	    {
		try
		{
		    string input = Console.ReadLine();
		    string output = CommandDispatcher.Dispatch(input);
		    Console.WriteLine(output);
		}
		catch (Exception exception)
		{
		    Console.WriteLine(exception.GetBaseException().Message);
		}
	    }
	}
    }
}
