using System;
using System.Threading.Tasks;
using App.Services.Identity.Domain;
using Framework.MongoDb;

namespace App.Services.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User> _repository;
        public UserRepository(IMongoRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<User> GetAsync(string email)
        {
            return await _repository.GetAsync(x => x.Email == email.ToLowerInvariant());
        }

        public async Task<User> GetAsync(Guid userId)
        {
            return await _repository.GetAsync(userId);
        }
    }
}
