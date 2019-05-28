using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Services.Interfaces
{
    public interface IUserService
    {
         Task<IEnumerable<UserDto>> GetAllUsers();
         Task<UserDto> GetUserById(int id);
    }
}