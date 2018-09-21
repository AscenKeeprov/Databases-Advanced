namespace PhotoShare.Services.Contracts
{
    using PhotoShare.Models;
    public interface ITagService
    {
	TModel ById<TModel>(int id);
	TModel ByName<TModel>(string name);
	bool Exists(int id);
	bool Exists(string name);
	Tag AddTag(string name);
    }
}
