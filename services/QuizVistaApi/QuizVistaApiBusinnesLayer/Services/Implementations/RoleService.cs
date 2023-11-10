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
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;


        public RoleService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ModelWithResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = await _roleRepository
                .GetAll()
                .OrderBy(x => x.RoleId)
                .ToListAsync();

            if (roles is null)
                throw new ArgumentNullException(nameof(roles));

            return new ModelWithResult<IEnumerable<Role>>(roles);
        }

        public async Task<ModelWithResult<Role>> GetRole(int roleId)
        {
            var role = await _roleRepository.GetAsync(roleId);

            if (role is null)
                throw new ArgumentNullException(nameof(role));

            return new ModelWithResult<Role>(role);
        }
    }
}
