using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.API.Data.Interfaces;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using DatingApp.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DatingApp.API.Services.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _photoRepo;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IOptions<Helpers.CloudnarySettings> _cloudnarySettings;
        private Cloudinary _cloudnary;
        public PhotoService(IPhotoRepository photoRepo,
                            IConfiguration configuration,
                            IUserService userService,
                            IMapper mapper,
                            IOptions<Helpers.CloudnarySettings> cloudnary)
        {
            _photoRepo = photoRepo;
            _configuration = configuration;
            _userService = userService;
            _mapper = mapper;
            _cloudnarySettings = cloudnary;

            Account acc = new Account(
                _cloudnarySettings.Value.CloudName,
                _cloudnarySettings.Value.ApiKey,
                _cloudnarySettings.Value.ApiSecret
            );

            _cloudnary = new Cloudinary(acc);
        }

        // public async Task<UserForDetailedDto> GetUserById(int id)
        // {
        //     UserForDetailedDto userForDetailedDto = await _userService.GetUserById(id);

        //     return userForDetailedDto;
        // }

        public async Task<PhotoForReturnDto> GetPhotoById(int id)
        {
            PhotoForReturnDto photoForReturnDto = new PhotoForReturnDto();

            var photo = await _photoRepo.GetPhotoById(id);

            photoForReturnDto = _mapper.Map(photo, photoForReturnDto);

            return photoForReturnDto;
        }

        public async Task<PhotoForReturnDto> AddUserPhoto(int id, PhotosForCreateDto photosForCreateDto)
        {

            Photo photoModel = new Photo();

            PhotoForReturnDto photoForReturnDto = new PhotoForReturnDto();

            UserForDetailedDto userForDetailedDto = new UserForDetailedDto();

            userForDetailedDto = await _userService.GetUserById(id);

            var file = photosForCreateDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File =  new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500)
                                            .Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudnary.Upload(uploadParams);

                }
            }

            photosForCreateDto.Url = uploadResult.Uri.ToString();
            photosForCreateDto.PublicId = uploadResult.PublicId;

            photoModel = _mapper.Map(photosForCreateDto, photoModel);

            if(!userForDetailedDto.Photos.Any(u => u.IsMain))
            {
                photoModel.IsMain = true;
            }

            photoModel.UserId = id;

            var photo = await _photoRepo.AddUserPhoto(photoModel);

            photoForReturnDto = _mapper.Map(photo, photoForReturnDto);

            return photoForReturnDto;
        }

        public async Task<bool> CheckUserPhoto(int userId, int photoId)
        {
            UserForDetailedDto userForDetailedDto = new UserForDetailedDto();

            userForDetailedDto = await _userService.GetUserById(userId);

            if (!userForDetailedDto.Photos.Any(p => p.Id == photoId))
            {
                return false;
            }

            return true;
        }

        public async Task<PhotoForReturnDto> GetMainPhotoForUser(int userId)
        {

            Photo photoModel = new Photo();

            PhotoForReturnDto photoForReturnDto = new PhotoForReturnDto();

            photoModel = await _photoRepo.GetMainPhotoForUser(userId);

            photoForReturnDto = _mapper.Map(photoModel, photoForReturnDto);

            return photoForReturnDto;
        }

        public async Task<bool> UnsetMainPhoto(PhotoForReturnDto photoForReturnDto)
        {
            Photo photoModel = new Photo();

            photoModel = _mapper.Map(photoForReturnDto, photoModel);

            var isValid = await _photoRepo.UnsetMainPhoto(photoModel);

            return isValid;
        }

        public async Task<bool> SetMainPhoto(PhotoToSetDto photoToSetDto)
        {
            Photo photoModel = new Photo();

            photoModel = _mapper.Map(photoToSetDto, photoModel);

            var isValid = await _photoRepo.SetMainPhoto(photoModel);

            return isValid;
        }
    }
}