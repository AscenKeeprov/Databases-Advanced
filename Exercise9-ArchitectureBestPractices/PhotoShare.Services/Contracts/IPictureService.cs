namespace PhotoShare.Services.Contracts
{
    using PhotoShare.Models;

    public interface IPictureService
    {
	TModel ById<TModel>(int id);
	TModel ByTitle<TModel>(string title);
	bool Exists(int id);
	bool Exists(string title);
	Picture Create(int albumId, string pictureTitle, string pictureFilePath);
    }
}
