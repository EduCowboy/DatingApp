using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.API.Dtos;
using DatingApp.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/user/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly IConfiguration _configuration;
        public PhotosController(IPhotoService photoService,
                                IConfiguration configuration)
        {
            _photoService = photoService;
            _configuration = configuration;
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<ActionResult> GetPhoto(int id)
        {
            var photo = await _photoService.GetPhotoById(id);

            return Ok(photo);
        }

        [HttpPost]
        public async Task<ActionResult> AddPhotoForUser(int userId, 
                                        [FromForm]PhotosForCreateDto photosForCreateDto)
        {
            PhotoForReturnDto photoForReturnDto = new PhotoForReturnDto();

            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            photoForReturnDto = await _photoService.AddUserPhoto(userId, photosForCreateDto);

            if (photoForReturnDto.Id != 0)
            {
                return CreatedAtRoute("GetPhoto", new {id = photoForReturnDto.Id}, photoForReturnDto);
            }

            return BadRequest("Could not add the photo");
        }

        [HttpPost("{id}/setMain")]
        public async Task<ActionResult> SetMainPhoto(int userId, int id)
        {
            PhotoToUnsetDto photoToUnset = new PhotoToUnsetDto();
            
            PhotoToSetDto photoToSetDto = new PhotoToSetDto();

            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var isUserPhoto = await _photoService.CheckUserPhoto(userId, id);

            if (!isUserPhoto)
            {
                return Unauthorized();
            }

            var photoToSet = await _photoService.GetPhotoById(id);

            if (photoToSet.IsMain)
            {
                return BadRequest("This is already the main photo!");
            }

            var currentMainPhoto = await _photoService.GetMainPhotoForUser(userId);
            
            currentMainPhoto.IsMain = false;

            var unsetMain = await _photoService.UnsetMainPhoto(currentMainPhoto);

            if (!unsetMain)
            {
                return BadRequest("Could not unset main photo");
            }
            else 
            {

                photoToSetDto.IsMain = true;
                photoToSetDto.UserId = userId;

                var setMain = await _photoService.SetMainPhoto(photoToSetDto);

                if (!setMain)
                {
                    return BadRequest("Could not set maisn photo");
                }
            }

            return NoContent();
        }
    }
}