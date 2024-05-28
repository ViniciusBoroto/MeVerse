using API.Models;
using API.Models.DTOs;
using AutoMapper;

namespace API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Post, PostViewModel>()
            // Where d is the destination prop, o is the options that you want to
            // configure and s is the source that will map to d
            .ForMember(d => d.User, o => o.MapFrom(s => new UserViewModel { UserName = s.User.UserName, ProfileImagePath = s.User.ProfileImagePath }))
            .ForMember(d => d.Text, o => o.MapFrom(s => s.Text))
            .ForMember(d => d.LikeAmount, o => o.MapFrom(s => s.LikedByUsers.Count())) ;
    }
}