using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data.Interfaces;
using DatingApp.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DatingApp.API.Services.Services
{
    public class DatingService : IDatingService
    {
        private readonly IDatingRepository _datingRepo;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public DatingService(IDatingRepository datingRepo,
                             IConfiguration configuration,
                             IMapper mapper)
        {
            _datingRepo = datingRepo;
            _configuration = configuration;
            _mapper = mapper;
        }

        public void Add<T>(T entity) where T : class
        {
            _datingRepo.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _datingRepo.Delete(entity);
        }

        public Task<bool> SaveAll()
        {
            return _datingRepo.SaveAll();
        }
    }
}