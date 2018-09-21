namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Client.Core.Exceptions;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class ModifyUserCommand : ICommand
    {
	private const string SuccessMessage = "User {0} {1} is {2}.";

	private readonly IUserService userService;
	private readonly ITownService townService;

	public ModifyUserCommand(IUserService userService, ITownService townService)
	{
	    this.userService = userService;
	    this.townService = townService;
	}

	// ModifyUser <username> <property> <new value>
	public string Execute(string[] data)
	{
	    string username = data[0];
	    ModifyUserDto userDTO = userService.ByUsername<ModifyUserDto>(username);
	    if (userDTO == null || userDTO.IsDeleted == true)
		throw new ObjectNotFoundException(typeof(User).Name, username);
	    var modifiableProperties = userDTO.GetType()
		.GetProperties().Select(pi => pi.Name);
	    string propertyName = data[1];
	    if (!modifiableProperties.Contains(propertyName))
		throw new PropertyNotSupportedException(propertyName);
	    string propertyValue = data[2];
	    switch (propertyName.ToUpper())
	    {
		case "BORNTOWN":
		    TownDto townDTO = townService.ByName<TownDto>(propertyValue);
		    if (townDTO == null) throw new InvalidValueException(
			propertyValue, Environment.NewLine +
			new ObjectNotFoundException(typeof(Town).Name, propertyValue).Message);
		    userService.SetBornTown(userDTO.Id, townDTO.Id);
		    break;
		case "CURRENTTOWN":
		    townDTO = townService.ByName<TownDto>(propertyValue);
		    if (townDTO == null) throw new InvalidValueException(
			propertyValue, Environment.NewLine +
			new ObjectNotFoundException(typeof(Town).Name, propertyValue).Message);
		    userService.SetCurrentTown(userDTO.Id, townDTO.Id);
		    break;
		case "PASSWORD":
		    if (!IsPasswordValid(propertyValue)) throw new InvalidValueException(
			propertyValue, Environment.NewLine +
			new InvalidObjectException("password").Message);
		    userService.ChangePassword(userDTO.Id, propertyValue);
		    break;
		default:
		    throw new PropertyNotSupportedException(propertyName);
	    }
	    return String.Format(SuccessMessage, username, propertyName, propertyValue);
	}

	private bool IsPasswordValid(string password)
	{
	    bool containsLowercase = password.Any(c => Char.IsLower(c));
	    bool containsDigit = password.Any(c => Char.IsDigit(c));
	    return containsLowercase && containsDigit;
	}
    }
}
