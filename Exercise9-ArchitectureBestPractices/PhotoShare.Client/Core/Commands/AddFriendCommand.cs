namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Client.Core.Exceptions;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class AddFriendCommand : ICommand
    {
	private const string SuccessMessage = "{0} sent a friendship request to {1}.";

	private readonly IUserService userService;

	public AddFriendCommand(IUserService userService)
	{
	    this.userService = userService;
	}

	// AddFriend <username1> <username2>
	public string Execute(string[] data)
	{
	    string userName = data[0];
	    UserFriendsDto userDTO = userService.ByUsername<UserFriendsDto>(userName);
	    if (userDTO == null || userDTO.IsDeleted == true)
		throw new ObjectNotFoundException(typeof(User).Name, userName);
	    string friendName = data[1];
	    UserFriendsDto friendDTO = userService.ByUsername<UserFriendsDto>(friendName);
	    if (friendDTO == null || friendDTO.IsDeleted == true)
		throw new ObjectNotFoundException(typeof(User).Name, friendName);
	    if (userDTO.Friends.Any(f => f.FriendId == friendDTO.Id))
	    {
		if (friendDTO.Friends.Any(f => f.FriendId == userDTO.Id))
		    throw new UsersAlreadyFriendsException(friendName, userName);
		else throw new FriendRequestExistsException(userName, friendName);
	    }
	    Friendship request = userService.AddFriend(userDTO.Id, friendDTO.Id);
	    return String.Format(SuccessMessage, userName, friendName);
	}
    }
}
