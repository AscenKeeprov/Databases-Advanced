using System;
using TeamBuilder.App.Core.Interfaces;
using TeamBuilder.Data.DataTransferObjects;
using TeamBuilder.Utilities;

namespace TeamBuilder.App.Core
{
    public class AuthenticationManager : IAuthenticationManager
    {
	private UserDto currentUser;

	public UserDto GetCurrentUser()
	{
	    if (!IsAuthenticated())
	    {
		throw new InvalidOperationException(Messages.LoginFirst);
	    }
	    return currentUser;
	}

	public bool IsAuthenticated()
	{
	    return currentUser != null;
	}

	public void Login(UserDto user)
	{
	    if (IsAuthenticated())
	    {
		throw new InvalidOperationException(Messages.LogoutFirst);
	    }
	    currentUser = user;
	}

	public void Logout()
	{
	    if (!IsAuthenticated())
	    {
		throw new InvalidOperationException(Messages.LoginFirst);
	    }
	    currentUser = null;
	}
    }
}
