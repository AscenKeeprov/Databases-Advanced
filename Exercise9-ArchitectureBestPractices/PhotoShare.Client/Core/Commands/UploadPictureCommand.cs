namespace PhotoShare.Client.Core.Commands
{
    using System;
    using PhotoShare.Client.Core.Contracts;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Client.Core.Exceptions;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;

    public class UploadPictureCommand : ICommand
    {
	private const string SuccessMessage = "Picture {0} added to album {1}!";

	private readonly IAlbumService albumService;
	private readonly IPictureService pictureService;

	public UploadPictureCommand(IAlbumService albumService, IPictureService pictureService)
	{
	    this.pictureService = pictureService;
	    this.albumService = albumService;
	}

	// UploadPicture <albumName> <pictureTitle> <pictureFilePath>
	public string Execute(string[] data)
	{
	    string albumName = data[0];
	    string pictureTitle = data[1];
	    string pictureFilePath = data[2];
	    AlbumDto albumDTO = albumService.ByName<AlbumDto>(albumName);
	    if (albumDTO == null) throw new ObjectNotFoundException(typeof(Album).Name, albumName);
	    var picture = pictureService.Create(albumDTO.Id, pictureTitle, pictureFilePath);
	    return String.Format(SuccessMessage, pictureTitle, albumName);
	}
    }
}
