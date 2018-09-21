namespace PhotoShare.Client.Core
{
    using AutoMapper;
    using PhotoShare.Client.Core.Dtos;
    using PhotoShare.Models;

    public class PhotoShareProfile : Profile
    {
	public PhotoShareProfile()
	{
	    CreateMap<Album, Album>();

	    CreateMap<Album, AlbumDto>().ReverseMap();

	    CreateMap<Album, AlbumTagsDto>()
		.ForMember(dto => dto.Tags, opt => opt.MapFrom(a => a.AlbumTags));

	    CreateMap<AlbumRole, AlbumRoleDto>()
		.ForMember(dto => dto.Username, opt => opt.MapFrom(ar => ar.User.Username))
		.ForMember(dto => dto.AlbumName, opt => opt.MapFrom(ar => ar.Album.Name))
		.ForMember(dto => dto.Permission, opt => opt.MapFrom(ar => ar.Role.ToString()))
		.ReverseMap();

	    CreateMap<AlbumTag, AlbumTagDto>()
		.ForMember(dto => dto.AlbumId, opt => opt.MapFrom(at => at.AlbumId))
		.ForMember(dto => dto.AlbumName, opt => opt.MapFrom(at => at.Album.Name))
		.ForMember(dto => dto.TagId, opt => opt.MapFrom(at => at.TagId))
		.ForMember(dto => dto.TagName, opt => opt.MapFrom(at => at.Tag.Name))
		.ReverseMap();

	    CreateMap<Friendship, FriendshipDto>()
		.ForMember(dto => dto.UserId, opt => opt.MapFrom(f => f.UserId))
		.ForMember(dto => dto.UserName, opt => opt.MapFrom(f => f.User.Username))
		.ForMember(dto => dto.FriendId, opt => opt.MapFrom(f => f.FriendId))
		.ForMember(dto => dto.FriendName, opt => opt.MapFrom(f => f.Friend.Username))
		.ReverseMap();

	    CreateMap<Tag, Tag>();

	    CreateMap<Tag, TagDto>().ReverseMap();

	    CreateMap<Town, Town>();

	    CreateMap<Town, TownDto>().ReverseMap();

	    CreateMap<User, ModifyUserDto>().ReverseMap();

	    CreateMap<User, RegisterUserDto>().ReverseMap();

	    CreateMap<User, User>();

	    CreateMap<User, UserDto>();

	    CreateMap<User, UserFriendsDto>()
		.ForMember(dto => dto.Friends, opt => opt.MapFrom(u => u.FriendsAdded));

	    CreateMap<User, UserRolesDto>()
		.ForMember(dto => dto.Permissions, opt => opt.MapFrom(u => u.AlbumRoles));
	}
    }
}
