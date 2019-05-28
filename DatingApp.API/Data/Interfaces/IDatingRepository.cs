using System.Threading.Tasks;

namespace DatingApp.API.Data.Interfaces
{
    public interface IDatingRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
    }
}