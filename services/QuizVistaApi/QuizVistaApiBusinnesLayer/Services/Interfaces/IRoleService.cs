using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface IRoleService
    {
        #region REST

        Task<ModelWithResult<IEnumerable<Role>>> GetRoles();
        Task<ModelWithResult<Role>> GetRole(int roleId);

        #endregion
    }
}
