using Formularies.UserManagementService.Core.Interfaces.Repositories;
using Formularies.UserManagementService.Core.Interfaces.Services;
using Formularies.UserManagementService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Services
{
    public class RoleService : IRoleService
    {
        public readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository=roleRepository??throw new ArgumentNullException(nameof(roleRepository));
        }
        public async Task<Role> CreateRole(Role role)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteRole(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
           return await _roleRepository.GetAllRoles();
        }

        public async Task<Role> GetRoleById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateRole(int id, Role role)
        {
            throw new NotImplementedException();
        }
    }
}
