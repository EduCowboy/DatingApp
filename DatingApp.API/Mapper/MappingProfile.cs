using System;
using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
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
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoURL, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt => {
                    opt.MapFrom(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoURL, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt => {
                    opt.MapFrom(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<Photo, PhotoDto>();
            CreateMap<Photo, PhotosForCreateDto>();
            CreateMap<Photo, PhotoForReturnDto>();

            CreateMap<PhotosForCreateDto, Photo>();
            CreateMap<PhotoForReturnDto, Photo>();
            CreateMap<PhotoDto, Photo>();

            CreateMap<PhotoToUnsetDto, Photo>();
            CreateMap<PhotoToSetDto, Photo>();

            CreateMap<UserForUpdateDto, User>();            
        }
    }
}