using System;
using API.DTOs;
using API.Entities;
using API.Helpers.Filtration;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, AppUser>();

            CreateMap<AppUser, UserDto>();
            CreateMap<AppUser, UserAdminDto>();
            CreateMap<AppUser, MemberDto>();

            CreateMap<FiltrationParams, FiltrationHeader>();
            CreateMap(typeof(FilteredList<,>), typeof(FilteredList<>));
        }
    }
}