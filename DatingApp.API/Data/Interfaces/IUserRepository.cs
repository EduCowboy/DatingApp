using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data.Interfaces
{
    public interface IUserRepository
    {
         //Task<int> AddUser(User user);
         //void DeleteUser(User user);
         //Task<User> UpdateUser(User user);
         Task<IEnumerable<User>> GetAllUsers();
         Task<User> GetUserById(int id);
    }
}