using System.Diagnostics.Metrics;
using AutoMapper;
using ProfileServiceApp.Dto;
using ProfileServiceApp.Models;

namespace ProfileServiceApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Follower, FollowerDto>(); // Map from Follower to FollowerDto
            CreateMap<Following, FollowingDto>(); // Map from Following to FollowingDto
            CreateMap<FollowingDto, Following>(); // Map from FollowingDto to Following
            CreateMap<ProfileDto, Profile>(); // Map from ProfileDto to Profile
            CreateMap<TrailDto, Trail>(); // Map from TrailDto to Trail
            CreateMap<FollowerDto, Follower>(); // Map from FollowerDto to Follower
            CreateMap<UserDto, User>(); // Map from UserDto to User
            CreateMap<UserStatDto, UserStat>(); // Map from UserStatDto to UserStat
            CreateMap<Profile, ProfileDto>(); // Map from Profile to ProfileDto
            CreateMap<Trail, TrailDto>(); // Map from Trail to TrailDto
            CreateMap<User, UserDto>(); // Map from User to UserDto
            CreateMap<UserStat, UserStatDto>(); // Map from UserStat to UserStatDto
        }
    }
}
