using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Data.Interfaces;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        /* public async Task<int> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            int id = user.Id;

            return id;
        } */

        // public void DeleteUser(User user)
        // {
        //     _context.Users.Remove(user);
        // }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var usersInfo = await _context.Users.Include(p => p.Photos).ToListAsync();
            
            return usersInfo;
        }

        public Task<User> GetUserById(int id)
        {
            var userInfo = _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

            return userInfo;
        }

         public async Task<bool> UpdateUser(User user)
         {
            try
            {
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