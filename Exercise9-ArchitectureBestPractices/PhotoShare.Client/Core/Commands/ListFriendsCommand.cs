namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Text;
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Client.Core.Exceptions;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class ListFriendsCommand : ICommand
    {
	private const string SuccessMessage = "Friends:{0}";

	private readonly IUserService userService;

	public ListFriendsCommand(IUserService userService)
	{
	    this.userService = userService;
	}

	// ListFriends <username>
	public string Execute(string[] data)
	{
	    string username = data[0];
	    UserFriendsDto userDTO = userService.ByUsername<UserFriendsDto>(username);
	    if (userDTO == null || userDTO.IsDeleted == true)
		throw new ObjectNotFoundException(typeof(User).Name, username);
	    if (userDTO.Friends?.Count == 0) throw new UserHasNoFriendsException(username);
	    StringBuilder friends = new StringBuilder(Environment.NewLine);
	    foreach (var friend in userDTO.Friends)
	    {
		friends.AppendLine($"-{friend.FriendName}");
	    }
	    return String.Format(SuccessMessage, friends.ToString().TrimEnd());
	}
    }
}
