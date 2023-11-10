using Microsoft.EntityFrameworkCore;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using QuizVistaApiInfrastructureLayer.Entities;
using QuizVistaApiInfrastructureLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUser(User user)
        {
            await _userRepository.InsertAsync(user);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<ModelWithResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userRepository
                .GetAll()
                .OrderBy(x => x.UserId)
                .ToListAsync();

            if (users is null)
                throw new ArgumentNullException(nameof(users));

            return new ModelWithResult<IEnumerable<User>>(users);
        }

        public async Task<ModelWithResult<User>> GetUser(int userId)
        {
            var user = await _userRepository.GetAsync(userId);

            if (user is null)
                throw new ArgumentNullException(nameof(user));

            return new ModelWithResult<User>(user);
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.UpdateAsync(user);
        }
    }
}
