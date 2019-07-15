using System.Threading.Tasks;
using DatingApp.API.Dtos;

namespace DatingApp.API.Services.Interfaces
{
    public interface IPhotoService
    {
        //Task<UserForDetailedDto> GetUserById(int id);
        Task<PhotoForReturnDto> AddUserPhoto(int userId, PhotosForCreateDto photosForCreateDto);
        Task<PhotoForReturnDto> GetPhotoById(int id);
        Task<bool> CheckUserPhoto(int userId, int photoId);
        Task<PhotoForReturnDto> GetMainPhotoForUser(int userId);
        Task<bool> UnsetMainPhoto(PhotoForReturnDto photoForReturnDto);
        Task<bool> SetMainPhoto(PhotoToSetDto photoToSetDto);
    }
}