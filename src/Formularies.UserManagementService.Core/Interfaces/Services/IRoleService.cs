using Formularies.UserManagementService.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Interfaces.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRoles();
        Task<Role> GetRoleById(int id);
        Task<Role> CreateRole(Role role);
        Task<bool> DeleteRole(int id);
        Task<bool> UpdateRole(int id, Role role);
    }
}
