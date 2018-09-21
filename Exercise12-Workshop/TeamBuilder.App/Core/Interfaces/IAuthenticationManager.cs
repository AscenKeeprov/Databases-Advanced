using TeamBuilder.Data.DataTransferObjects;

namespace TeamBuilder.App.Core.Interfaces
{
    public interface IAuthenticationManager
    {
	UserDto GetCurrentUser();
	bool IsAuthenticated();
	void Login(UserDto user);
	void Logout();
    }
}
