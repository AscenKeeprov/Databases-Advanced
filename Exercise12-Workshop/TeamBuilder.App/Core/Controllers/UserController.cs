using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Data;
using TeamBuilder.Data.DataTransferObjects;
using TeamBuilder.Models;
using TeamBuilder.Models.Enumerations;

namespace TeamBuilder.App.Core.Controllers
{
    public class UserController : IUserController
    {
	private readonly TeamBuilderDbContext dbContext;

	public UserController(TeamBuilderDbContext dbContext)
	{
	    this.dbContext = dbContext;
	}

	public void DeleteUser(int userId)
	{
	    User user = dbContext.Users.Find(userId);
	    user.IsDeleted = true;
	    dbContext.SaveChanges();
	}

	public UserDto GetUser(string username)
	{
	    User user = dbContext.Users.FirstOrDefault(u => u.Username == username && !u.IsDeleted);
	    if (user != null)
	    {
		UserDto userDto = new UserDto()
		{
		    Id = user.Id,
		    Username = user.Username,
		    Password = user.Password,
		    IsDeleted = user.IsDeleted
		};
		return userDto;
	    }
	    else return null;
	}

	public void RegisterUser(string username, string password,
	    string firstName, string lastName, int age, Gender gender)
	{
	    User user = new User()
	    {
		Username = username,
		Password = password,
		FirstName = firstName,
		LastName = lastName,
		Age = age,
		Gender = gender
	    };
	    if (IsValid(user))
	    {
		dbContext.Users.Add(user);
		dbContext.SaveChanges();
	    }
	}

	public bool UserExists(string username)
	{
	    return dbContext.Users.Any(u => u.Username == username && !u.IsDeleted);
	}

	private static bool IsValid(User user)
	{
	    var validationContext = new ValidationContext(user);
	    var validationResults = new List<ValidationResult>();
	    bool isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);
	    if (!isValid) throw new ArgumentException(validationResults.First().ErrorMessage);
	    return isValid;
	}
    }
}
