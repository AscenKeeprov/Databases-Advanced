namespace PhotoShare.Services.Contracts
{
    using PhotoShare.Models;

    public interface IAlbumService
    {
	TModel ById<TModel>(int id);
	TModel ByName<TModel>(string name);
	bool Exists(int id);
	bool Exists(string name);
	Album Create(int userId, string albumName, string backgroundColor, string[] tagNames);
    }
}
