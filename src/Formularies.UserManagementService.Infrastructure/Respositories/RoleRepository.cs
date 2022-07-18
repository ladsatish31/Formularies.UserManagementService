using AutoMapper;
using Formularies.UserManagementService.Core.Interfaces.Repositories;
using Formularies.UserManagementService.Core.Models;
using Formularies.UserManagementService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Infrastructure.Respositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _dbcontext;
        private readonly IMapper _mapper;
        public RoleRepository(AppDbContext dbcontext,IMapper mapper)
        {
            _dbcontext = dbcontext??throw new ArgumentNullException(nameof(dbcontext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            var role = await _dbcontext.Roles.ToListAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<Role>>(role);
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
