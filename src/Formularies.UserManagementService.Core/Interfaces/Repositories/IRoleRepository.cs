using Formularies.UserManagementService.Core.Models;
using Formularies.UserManagementService.Core.Request;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        //Task<IEnumerable<Role>> GetAllRoles();
        Task<Role> GetRoleById(int id);
        Task<RoleRequest> CreateRole(RoleRequest role);
        Task<bool> DeleteRole(int id);
        Task<bool> UpdateRole(int id, RoleRequest role);
        IQueryable<Role> GetAllRoles();

    }
}
