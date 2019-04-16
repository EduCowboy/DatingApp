using System.Threading.Tasks;
using DatingApp.API.Data.Interfaces;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //validate request
 
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if(await _authRepo.UserExsists(userForRegisterDto.Username))
            {
                return BadRequest("Username already taken!");
            }

            var UserToCreate = new User
            {
                UserName = userForRegisterDto.Username
            };

            var createUser = await _authRepo.Register(UserToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }
    }
}