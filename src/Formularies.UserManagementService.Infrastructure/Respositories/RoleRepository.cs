using AutoMapper;
using AutoWrapper.Wrappers;
using Formularies.UserManagementService.Core.Interfaces.Repositories;
using Formularies.UserManagementService.Core.Models;
using Formularies.UserManagementService.Core.Request;
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

        public async Task<RoleRequest> CreateRole(RoleRequest role)
        {
            if (_dbcontext.Roles.Any(x => x.RoleName == role.RoleName))
                throw new ApiException($"Role '{role.RoleName}' is already registered");
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
                throw new ApiException($"Role id '{id}' not found",404);

            }
            if (roleToDelete != null)
            {
                _dbcontext.Entry(roleToDelete).State= EntityState.Modified;
                _dbcontext.Roles.Remove(roleToDelete);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //public async Task<IEnumerable<Role>> GetAllRoles()
        //{
        //    var dbRoles = await _dbcontext.Roles.ToListAsync().ConfigureAwait(false);
        //    if (dbRoles.Count>0)
        //    {
        //        return _mapper.Map<IEnumerable<Role>>(dbRoles);
        //    }
        //    return null;
        //}

        public IQueryable<Role> GetAllRoles()
        {
            var dbRoles = _dbcontext.Roles.AsQueryable();            
            if (dbRoles!=null)
            {
                return _mapper.Map<IEnumerable<Role>>(dbRoles).AsQueryable();
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
            else
            throw new ApiException($"Role id '{id}' not found", 404);
        }

        public async Task<bool> UpdateRole(int id, RoleRequest role)
        {
            var roleToUpdate=await _dbcontext.Roles.FindAsync(id);
            if(roleToUpdate == null)
            {
                throw new ApiException($"Role id '{id}' not found", 404);
            }
            if (roleToUpdate.RoleName != role.RoleName && _dbcontext.Roles.Any(x => x.RoleName == role.RoleName))
                throw new ApiException($"Role '{role.RoleName}' is already taken");
            _dbcontext.Entry(roleToUpdate).State= EntityState.Modified;
            roleToUpdate.RoleName = role.RoleName;
            roleToUpdate.RoleDescription = role.RoleDescription;           
            roleToUpdate.UpdatedDate = DateTime.Now;
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
