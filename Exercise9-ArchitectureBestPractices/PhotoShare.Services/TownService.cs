namespace PhotoShare.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class TownService : ITownService
    {
	private readonly PhotoShareContext context;

	public TownService(PhotoShareContext context)
	{
	    this.context = context;
	}

	public bool Exists(int id) => ById<Town>(id) != null;
	public bool Exists(string name) => ByName<Town>(name) != null;

	private IEnumerable<TModel> By<TModel>(Func<Town, bool> predicate)
	    => context.Towns.Where(predicate).AsQueryable().ProjectTo<TModel>();

	public TModel ById<TModel>(int id)
	    => By<TModel>(t => t.Id == id).SingleOrDefault();

	public TModel ByName<TModel>(string name)
	    => By<TModel>(t => t.Name == name).SingleOrDefault();

	public Town Add(string townName, string countryName)
	{
	    Town town = new Town()
	    {
		Name = townName,
		Country = countryName,
	    };
	    context.Towns.Add(town);
	    context.SaveChanges();
	    return town;
	}
    }
}
