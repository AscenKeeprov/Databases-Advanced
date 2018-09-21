namespace PhotoShare.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class UserService : IUserService
    {
	private readonly PhotoShareContext context;

	public UserService(PhotoShareContext context)
	{
	    this.context = context;
	}

	public bool Exists(int id) => ById<User>(id) != null;
	public bool Exists(string username) => ByUsername<User>(username) != null;

	private IEnumerable<TModel> By<TModel>(Func<User, bool> predicate)
	    => context.Users.Where(predicate).AsQueryable().ProjectTo<TModel>();

	public TModel ById<TModel>(int id)
	    => By<TModel>(u => u.Id == id).SingleOrDefault();

	public TModel ByUsername<TModel>(string username)
	    => By<TModel>(u => u.Username == username).SingleOrDefault();

	public Friendship AcceptFriend(int userId, int friendId)
	{
	    Friendship friendship = new Friendship()
	    {
		UserId = userId,
		FriendId = friendId
	    };
	    context.Friendships.Add(friendship);
	    context.SaveChanges();
	    return friendship;
	}

	public Friendship AddFriend(int userId, int friendId)
	{
	    Friendship friendship = new Friendship()
	    {
		UserId = userId,
		FriendId = friendId
	    };
	    context.Friendships.Add(friendship);
	    context.SaveChanges();
	    return friendship;
	}

	public void ChangePassword(int userId, string password)
	{
	    User user = context.Users.Find(userId);
	    user.Password = password;
	    context.SaveChanges();
	}

	public void Delete(string username)
	{
	    int userId = ByUsername<User>(username).Id;
	    User user = context.Users.Find(userId);
	    user.IsDeleted = true;
	    context.SaveChanges();
	}

	public User Register(string username, string password, string email)
	{
	    User user = new User()
	    {
		Username = username,
		Password = password,
		Email = email,
		IsDeleted = false
	    };
	    context.Users.Add(user);
	    context.SaveChanges();
	    return user;
	}

	public void SetBornTown(int userId, int townId)
	{
	    User user = context.Users.Find(userId);
	    user.BornTownId = townId;
	    context.SaveChanges();
	}

	public void SetCurrentTown(int userId, int townId)
	{
	    User user = context.Users.Find(userId);
	    user.CurrentTownId = townId;
	    context.SaveChanges();
	}
    }
}
