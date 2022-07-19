using Formularies.UserManagementService.Core.Interfaces.Repositories;
using Formularies.UserManagementService.Core.Interfaces.Services;
using Formularies.UserManagementService.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Services
{
    public class RoleService : IRoleService
    {
        public readonly IRoleRepository _roleRepository;
        public readonly ILogger<RoleService> _logger;
        public RoleService(IRoleRepository roleRepository, ILogger<RoleService> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Role> CreateRole(Role role)
        {
            try
            {
                return await _roleRepository.CreateRole(role);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call CreateRole in service class, Error message={ex}.");
                throw;
            }
        }

        public async Task<bool> DeleteRole(int id)
        {
            try
            {
                return await _roleRepository.DeleteRole(id);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call DeleteRole in service class, Error message={ex}.");
                throw;
            }
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            try
            {
                return await _roleRepository.GetAllRoles();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call GetAllRoles in service class, Error message={ex}.");
                throw;
            }
        }

        public async Task<Role> GetRoleById(int id)
        {
            try
            {
                return await _roleRepository.GetRoleById(id);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call GetRoleById in service class, Error message={ex}.");
                throw;
            }
        }

        public async Task<bool> UpdateRole(int id, Role role)
        {
            try
            {
                return await _roleRepository.UpdateRole(id, role);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call UpdateRole in service class, Error message={ex}.");
                throw;
            }
        }
    }
}
