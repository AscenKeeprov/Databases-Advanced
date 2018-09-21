using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DeltaTradeOnlineLTD.Services.Interfaces;
using DeltaTradeOnlineLTD.Models;
using DeltaTradeOnlineLTD.Models.Interfaces;
using DeltaTradeOnlineLTD.Services.Enumerations;

namespace DeltaTradeOnlineLTD.Services
{
    public class CommandFactory : ICommandFactory
    {
	private const string InvalidCommandException = "[{0}] is not a valid command!";

	Assembly Assembly => Assembly.GetEntryAssembly();
	Type[] CommandTypes => Assembly.GetTypes().Where(type
	    => type.BaseType == typeof(Command)).ToArray();

	private readonly IServiceProvider server;

	public CommandFactory(IServiceProvider serviceProvider)
	{
	    server = serviceProvider;
	}

	public ICommand CreateCommand(ICollection<string> commandInput)
	{
	    if (!Enum.TryParse(typeof(CommandType), commandInput.First(), true, out object commandName))
		throw new ArgumentException(String.Format(InvalidCommandException, commandInput.First()));
	    Type commandType = CommandTypes.FirstOrDefault(t => t.Name.Equals($"{commandName}{typeof(Command).Name}"));
	    ConstructorInfo commandConstructor = commandType.GetConstructors().First();
	    ParameterInfo[] constructorParameters = commandConstructor.GetParameters();
	    IList<object> commandDependencies = new List<object>();
	    for (int i = 0; i < constructorParameters.Length; i++)
	    {
		Type serviceType = constructorParameters[i].ParameterType;
		object service = server.GetService(serviceType);
		if (service != null) commandDependencies.Add(service);
	    }
	    object[] commandParameters = commandDependencies.Concat(commandInput.Skip(1)).ToArray();
	    try
	    {
		ICommand command = (ICommand)Activator.CreateInstance(commandType, commandParameters);
		return command;
	    }
	    catch (TargetInvocationException tie)
	    {
		throw tie.InnerException;
	    }
	}
    }
}
