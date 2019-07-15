using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data.Interfaces
{
    public interface IPhotoRepository
    {
         Task<Photo> AddUserPhoto(Photo photo);
         Task<Photo> GetPhotoById(int id);
         Task<Photo> GetMainPhotoForUser(int userId);
         Task<bool> UnsetMainPhoto(Photo photo);
         Task<bool> SetMainPhoto(Photo photo);
    }
}