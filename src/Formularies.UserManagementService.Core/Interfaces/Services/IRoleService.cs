using Formularies.UserManagementService.Core.Models;
using Formularies.UserManagementService.Core.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Interfaces.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAllRoles(SearchFilter searchFilter);
        Task<Role> GetRoleById(int id);
        Task<RoleRequest> CreateRole(RoleRequest role);
        Task<bool> DeleteRole(int id);
        Task<bool> UpdateRole(int id, RoleRequest role);
    }
}
