using TeamBuilder.Data.DataTransferObjects;
using TeamBuilder.Models.Enumerations;

namespace TeamBuilder.App.Core.Interfaces
{
    public interface IUserController
    {
	void DeleteUser(int userId);
	UserDto GetUser(string username);
	void RegisterUser(string username, string password,
	    string firstName, string lastName, int age, Gender gender);
	bool UserExists(string username);
    }
}
