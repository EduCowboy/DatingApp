using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data.Interfaces;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext _context;

        public PhotoRepository(DataContext context)
        {
            _context = context;
        } 

        public async Task<Photo> AddUserPhoto(Photo photo)
        {
            await _context.Photos.AddAsync(photo);
            await _context.SaveChangesAsync();

            return photo;
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            var photoInfo = await _context.Photos.Where(p => p.Id == userId)
                    .FirstOrDefaultAsync(p => p.IsMain);

            return photoInfo;
        }

        public async Task<Photo> GetPhotoById(int id)
        {
            var photoInfo = await _context.Photos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            return photoInfo;
        }

        public async Task<bool> UnsetMainPhoto(Photo photo)
        {
            try
            {
                _context.Photos.Attach(photo);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SetMainPhoto(Photo photo)
        {
            try
            {
                //_context.Photos.Update(photo);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}