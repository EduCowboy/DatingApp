using System.Threading.Tasks;
using DatingApp.API.Dtos;

namespace DatingApp.API.Services.Interfaces
{
    public interface IAuthService
    {
         Task<bool> UserExsists(UserForRegisterDto userForRegisterDto);
         Task<UserForLoginDto> Login(UserForLoginDto userForLoginDto);
         Task<UserDto> Register(UserDto userForLoginDto, string password);
    }
}