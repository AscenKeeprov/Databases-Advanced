namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Client.Core.Exceptions;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class AcceptFriendCommand : ICommand
    {
	private const string SuccessMessage = "{0} accepted {1} as a friend.";

	private readonly IUserService userService;

	public AcceptFriendCommand(IUserService userService)
	{
	    this.userService = userService;
	}

	// AcceptFriend <username1> <username2>
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
	    if (!friendDTO.Friends.Any(f => f.FriendId == userDTO.Id))
		throw new FriendRequestMissingException(friendName, userName);
	    if (userDTO.Friends.Any(f => f.FriendId == friendDTO.Id))
		throw new UsersAlreadyFriendsException(friendName, userName);
	    Friendship confirmation = userService.AcceptFriend(userDTO.Id, friendDTO.Id);
	    return String.Format(SuccessMessage, userName, friendName);
	}
    }
}
