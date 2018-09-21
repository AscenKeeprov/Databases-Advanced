namespace PhotoShare.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class TagService : ITagService
    {
	private readonly PhotoShareContext context;

	public TagService(PhotoShareContext context)
	{
	    this.context = context;
	}

	public bool Exists(int id) => ById<Tag>(id) != null;
	public bool Exists(string name) => ByName<Tag>(name) != null;

	private IEnumerable<TModel> By<TModel>(Func<Tag, bool> predicate)
	    => context.Tags.Where(predicate).AsQueryable().ProjectTo<TModel>();

	public TModel ById<TModel>(int id)
	    => By<TModel>(t => t.Id == id).SingleOrDefault();

	public TModel ByName<TModel>(string name)
	    => By<TModel>(t => t.Name == name).SingleOrDefault();

	public Tag AddTag(string name)
	{
	    Tag tag = new Tag()
	    {
		Name = name
	    };
	    context.Tags.Add(tag);
	    context.SaveChanges();
	    return tag;
	}
    }
}
