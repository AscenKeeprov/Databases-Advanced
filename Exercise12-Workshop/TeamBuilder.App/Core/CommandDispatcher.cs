using System;
using System.Linq;
using TeamBuilder.App.Core.Commands;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Utilities;

namespace TeamBuilder.App.Core
{
    public class CommandDispatcher : ICommandDispatcher
    {
	private readonly IServiceProvider serviceProvider;

	public CommandDispatcher(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public string Dispatch(string input)
	{
	    string output = String.Empty;
	    string[] inputArgs = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
	    string commandName = inputArgs.Length > 0 ? inputArgs[0] : String.Empty;
	    string[] commandArgs = inputArgs.Skip(1).ToArray();
	    ICommand command = null;
	    switch (commandName)
	    {
		case "AcceptInvite":
		    command = new AcceptInviteCommand(serviceProvider);
		    break;
		case "AddTeamTo":
		    command = new AddTeamToCommand(serviceProvider);
		    break;
		case "CreateEvent":
		    command = new CreateEventCommand(serviceProvider);
		    break;
		case "CreateTeam":
		    command = new CreateTeamCommand(serviceProvider);
		    break;
		case "DeclineInvite":
		    command = new DeclineInviteCommand(serviceProvider);
		    break;
		case "DeleteUser":
		    command = new DeleteUserCommand(serviceProvider);
		    break;
		case "Disband":
		    command = new DisbandCommand(serviceProvider);
		    break;
		case "Exit":
		    command = new ExitCommand();
		    break;
		case "InviteToTeam":
		    command = new InviteToTeamCommand(serviceProvider);
		    break;
		case "KickMember":
		    command = new KickMemberCommand(serviceProvider);
		    break;
		case "Login":
		    command = new LoginCommand(serviceProvider);
		    break;
		case "Logout":
		    command = new LogoutCommand(serviceProvider);
		    break;
		case "RegisterUser":
		    command = new RegisterUserCommand(serviceProvider);
		    break;
		case "ShowEvent":
		    command = new ShowEventCommand(serviceProvider);
		    break;
		case "ShowTeam":
		    command = new ShowTeamCommand(serviceProvider);
		    break;
		default:
		    throw new NotSupportedException(String.Format(Messages.CommandNotSupported, commandName));
	    }
	    output = command.Execute(commandArgs);
	    return output;
	}
    }
}
