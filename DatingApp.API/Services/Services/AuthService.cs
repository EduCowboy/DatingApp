using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data.Interfaces;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using DatingApp.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DatingApp.API.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthService(IAuthRepository authRepo, IConfiguration config, IMapper mapper)
        {
            _authRepo = authRepo;
            _config = config;
            _mapper = mapper;
        }

        public async Task<bool> UserExsists(UserForRegisterDto userForRegisterDto)
        {
            bool userExists = false;

            userExists = await _authRepo.UserExsists(userForRegisterDto.Username);

            return userExists;
        }

        public async Task<UserForLoginDto> Login(UserForLoginDto userForLoginDto)
        {
            User userModel = await _authRepo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            userForLoginDto = _mapper.Map<UserForLoginDto>(userModel);

            return userForLoginDto;
        }

        public async Task<UserDto> Register(UserDto userDto, string password)
        {
           User userModel = _mapper.Map<User>(userDto);

           userModel = await _authRepo.Register(userModel, password);

           userDto = _mapper.Map<UserDto>(userModel);

            return userDto;
        }
    }
}