using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface IUserService
    {
        #region REST

        Task<ModelWithResult<IEnumerable<User>>> GetUsers();
        Task<ModelWithResult<User>> GetUser(int UsertId);
        Task CreateUser(User user);
        Task DeleteUser(int UsertId);
        Task UpdateUser(User user);

        #endregion
    }
}