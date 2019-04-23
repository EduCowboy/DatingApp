using System;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, UserForLoginDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            //CreateMap<UserDto, User>();
        }
    }
}