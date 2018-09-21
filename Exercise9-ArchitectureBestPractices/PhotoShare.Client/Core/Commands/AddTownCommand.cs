namespace PhotoShare.Client.Core.Commands
{
    using System;
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Client.Core.Exceptions;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class AddTownCommand : ICommand
    {
	private const string SuccessMessage = "Town {0} was added successfully!";

	private readonly ITownService townService;

	public AddTownCommand(ITownService townService)
	{
	    this.townService = townService;
	}

	// AddTown <townName> <countryName>
	public string Execute(string[] data)
	{
	    string townName = data[0];
	    if (townService.Exists(townName))
		throw new DuplicateObjectException(typeof(Town).Name, townName);
	    string countryName = data[1];
	    Town town = townService.Add(townName, countryName);
	    return String.Format(SuccessMessage, town.Name);
	}
    }
}
