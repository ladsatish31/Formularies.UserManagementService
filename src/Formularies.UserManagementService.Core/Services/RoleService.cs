using AutoWrapper.Wrappers;
using Formularies.UserManagementService.Core.Interfaces.Repositories;
using Formularies.UserManagementService.Core.Interfaces.Services;
using Formularies.UserManagementService.Core.Models;
using Formularies.UserManagementService.Core.Request;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
        public async Task<RoleRequest> CreateRole(RoleRequest role)
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

        public IEnumerable<Role> GetAllRoles(SearchFilter searchFilter)
        {
            try
            {
                var allRoles = _roleRepository.GetAllRoles();
               
                if (!string.IsNullOrEmpty(searchFilter.Search))
                {
                    allRoles = allRoles.Where(r => r.RoleName.Contains(searchFilter.Search) || r.RoleDescription.Contains(searchFilter.Search));
                }

                //Default sort by role
                allRoles = allRoles.OrderBy(hh => hh.RoleId);
                if (!string.IsNullOrEmpty(searchFilter.SortBy))
                {
                    switch (searchFilter.SortBy)
                    {
                        case "id_asc": allRoles = allRoles.OrderBy(hh => hh.RoleId); break;
                        case "id_desc": allRoles = allRoles.OrderByDescending(hh => hh.RoleId); break;
                        case "name_asc": allRoles = allRoles.OrderBy(hh => hh.RoleName); break;
                        case "name_desc": allRoles = allRoles.OrderByDescending(hh => hh.RoleName); break;
                        case "des_asc": allRoles = allRoles.OrderBy(hh => hh.RoleDescription); break;
                        case "des_desc": allRoles = allRoles.OrderByDescending(hh => hh.RoleDescription); break;
                    }
                }                
                return allRoles.Select(role => new Role
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    RoleDescription = role.RoleDescription                    
                }).ToList();
                
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

        public async Task<bool> UpdateRole(int id, RoleRequest role)
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
