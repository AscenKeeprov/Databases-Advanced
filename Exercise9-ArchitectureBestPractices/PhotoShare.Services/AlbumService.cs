namespace PhotoShare.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using PhotoShare.Models.Enums;
    using PhotoShare.Services.Contracts;

    public class AlbumService : IAlbumService
    {
	private readonly PhotoShareContext context;

	public AlbumService(PhotoShareContext context)
	{
	    this.context = context;
	}

	public bool Exists(int id) => ById<Album>(id) != null;
	public bool Exists(string name) => ByName<Picture>(name) != null;

	private IEnumerable<TModel> By<TModel>(Func<Album, bool> predicate)
	    => context.Albums.Where(predicate).AsQueryable().ProjectTo<TModel>();

	public TModel ById<TModel>(int id)
	    => By<TModel>(a => a.Id == id).SingleOrDefault();

	public TModel ByName<TModel>(string name)
	    => By<TModel>(a => a.Name == name).SingleOrDefault();

	public Album Create(int userId, string albumName, string backgroundColor, string[] tagNames)
	{
	    Album album = new Album()
	    {
		Name = albumName,
		BackgroundColor = Enum.Parse<Color>(backgroundColor, true),
	    };
	    context.Albums.Add(album);
	    context.SaveChanges();
	    AlbumRole albumRole = new AlbumRole()
	    {
		AlbumId = album.Id,
		UserId = userId,
		Role = Role.Owner
	    };
	    context.AlbumRoles.Add(albumRole);
	    context.SaveChanges();
	    foreach (var tagName in tagNames)
	    {
		Tag tag = context.Tags.FirstOrDefault(t => t.Name == tagName);
		AlbumTag albumTag = new AlbumTag()
		{
		    AlbumId = album.Id,
		    TagId = tag.Id
		};
		context.AlbumTags.Add(albumTag);
	    }
	    context.SaveChanges();
	    return album;
	}
    }
}
