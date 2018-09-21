namespace PhotoShare.Services.Contracts
{
    using PhotoShare.Models;

    public interface ITownService
    {
	TModel ById<TModel>(int id);
	TModel ByName<TModel>(string name);
	bool Exists(int id);
	bool Exists(string name);
	Town Add(string townName, string countryName);
    }
}
