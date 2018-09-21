namespace PhotoShare.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class PictureService : IPictureService
    {
	private readonly PhotoShareContext context;

	public PictureService(PhotoShareContext context)
	{
	    this.context = context;
	}

	public bool Exists(int id) => ById<Picture>(id) != null;
	public bool Exists(string title) => ByTitle<Picture>(title) != null;

	private IEnumerable<TModel> By<TModel>(Func<Picture, bool> predicate)
	    => context.Pictures.Where(predicate).AsQueryable().ProjectTo<TModel>();

	public TModel ById<TModel>(int id)
	    => By<TModel>(p => p.Id == id).SingleOrDefault();

	public TModel ByTitle<TModel>(string title)
	    => By<TModel>(p => p.Title == title).SingleOrDefault();

	public Picture Create(int albumId, string pictureTitle, string pictureFilePath)
	{
	    Picture picture = new Picture()
	    {
		AlbumId = albumId,
		Title = pictureTitle,
		Path = pictureFilePath
	    };
	    context.Pictures.Add(picture);
	    context.SaveChanges();
	    return picture;
	}
    }
}
