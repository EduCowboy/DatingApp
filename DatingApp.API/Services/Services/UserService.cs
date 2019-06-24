using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data.Interfaces;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using DatingApp.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DatingApp.API.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepo,
                             IConfiguration configuration,
                             IMapper mapper)
        {
            _userRepo = userRepo;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserForListDto>> GetAllUsers()
        {
            IEnumerable<User> userModel = await _userRepo.GetAllUsers();

            IEnumerable<UserForListDto> userForListDto = _mapper.Map<List<UserForListDto>>(userModel);

            return userForListDto;
        }

        public async Task<UserForDetailedDto> GetUserById(int id)
        {
            User userModel =  await _userRepo.GetUserById(id);

            UserForDetailedDto userForDetailedDto = _mapper.Map<UserForDetailedDto>(userModel);

            return userForDetailedDto;
        }

        public async Task<bool> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            bool isValid = false;

            User userModel =  await _userRepo.GetUserById(id);

            _mapper.Map(userForUpdateDto, userModel);

            isValid = await _userRepo.UpdateUser(userModel);
            
            return isValid;
        }
    }
}