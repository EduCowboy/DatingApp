using System.Threading.Tasks;

namespace DatingApp.API.Services.Interfaces
{
    public interface IDatingService
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
    }
}