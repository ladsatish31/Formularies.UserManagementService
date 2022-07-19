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
            var dbRole=_mapper.Map<Entities.Role>(role);
            await _dbcontext.Roles.AddAsync(dbRole);
            await _dbcontext.SaveChangesAsync();
            role.RoleId = dbRole.RoleId;
            return role;
        }

        public async Task<bool> DeleteRole(int id)
        {
           var roleToDelete=await _dbcontext.Roles.FindAsync(id);
            if(roleToDelete == null)
            {
                return false;
            }
            if(roleToDelete != null)
            {
                _dbcontext.Entry(roleToDelete).State= EntityState.Modified;
                _dbcontext.Roles.Remove(roleToDelete);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            var dbRoles = await _dbcontext.Roles.ToListAsync().ConfigureAwait(false);
            if (dbRoles != null)
            {
                return _mapper.Map<IEnumerable<Role>>(dbRoles);
            }
            return null;
        }

        public async Task<Role> GetRoleById(int id)
        {
           var dbRole=await _dbcontext.Roles.FindAsync(id);
            if (dbRole != null)
            {
                return _mapper.Map<Role>(dbRole);
            }
            return null;
        }

        public async Task<bool> UpdateRole(int id, Role role)
        {
            var roleToUpdate=await _dbcontext.Roles.FindAsync(id);
            if(roleToUpdate == null)
            {
                return false;
            }
            if (roleToUpdate == null||roleToUpdate.RoleId!=id)
            {
                return false;
            }
            _dbcontext.Entry(roleToUpdate).State= EntityState.Modified;
            roleToUpdate.RoleName = role.RoleName;
            roleToUpdate.RoleDescription = role.RoleDescription;           
            roleToUpdate.UpdatedDate = DateTime.UtcNow;
            if(role!=null)
            {
                _dbcontext.Roles.Update(roleToUpdate);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
