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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbcontext;
        private readonly IMapper _mapper;
        public UserRepository(AppDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<UserCreateRequest> CreateUser(UserCreateRequest user)
        {
            if (_dbcontext.Users.Any(x => x.Email == user.Email))
                throw new ApiException($"Email '{user.Email}' is already registered");
            var userTocreate = _mapper.Map<Entities.User>(user);
            await _dbcontext.Users.AddAsync(userTocreate);
            await _dbcontext.SaveChangesAsync();
            user.UserId = userTocreate.UserId;
            return user;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var userToDelete = await _dbcontext.Users.FindAsync(id);
            if (userToDelete == null)
            {
                throw new ApiException($"User id '{id}' not found", 404);
            }
            if (userToDelete != null)
            {
                _dbcontext.Entry(userToDelete).State = EntityState.Modified;
                _dbcontext.Users.Remove(userToDelete);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //public async Task<IEnumerable<User>> GetAllUsers()
        //{
        //    var dbUsers = await _dbcontext.Users.ToListAsync().ConfigureAwait(false);
        //    if (dbUsers.Count > 0)
        //    {
        //        return _mapper.Map<IEnumerable<User>>(dbUsers);
        //    }
        //    return null;
        //}
        public IQueryable<User> GetAllUsers()
        {
            var dbUsers = _dbcontext.Users.AsQueryable();
            if (dbUsers != null)
            {
                return _mapper.Map<IEnumerable<User>>(dbUsers).AsQueryable();
            }
            return null;
        }

        public async Task<User> GetUserById(Guid id)
        {
            var dbUser = await _dbcontext.Users.FindAsync(id);
            if (dbUser != null)
            {
                return _mapper.Map<User>(dbUser);
            }
            throw new ApiException($"User id '{id}' not found", 404);
        }

        public async Task<bool> UpdateUser(Guid id, UserUpdateRequest user)
        {
            var updatedUser = _mapper.Map<Entities.User>(user);
            var userToUpdate = await _dbcontext.Users.FindAsync(id);
            if (userToUpdate == null)
            {
                throw new ApiException($"User id '{id}' not found", 404);
            }
            
            _dbcontext.Entry(userToUpdate).State = EntityState.Modified;           
            userToUpdate.Name = updatedUser.Name;
            userToUpdate.Email = updatedUser.Email;
            userToUpdate.IsActive = updatedUser.IsActive;
            userToUpdate.RoleId = updatedUser.RoleId;
            userToUpdate.UpdatedDate = DateTime.Now;
            userToUpdate.ResetToken = updatedUser.ResetToken;
            userToUpdate.ResetTokenExpiryDate = updatedUser.ResetTokenExpiryDate;
            userToUpdate.PasswordHash = updatedUser.PasswordHash;
            userToUpdate.PasswordResetDate = updatedUser.PasswordResetDate;
            userToUpdate.RefreshTokens = updatedUser.RefreshTokens;
            if (userToUpdate != null)
            {
                _dbcontext.Users.Update(userToUpdate);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var dbUser = await _dbcontext.Users.SingleOrDefaultAsync(x => x.Email == email);
            if (dbUser != null)
            {
                return _mapper.Map<User>(dbUser);
            }
            return null;
        }

        public async Task<User> GetUserByResetToken(string token)
        {
            var dbUser = await _dbcontext.Users.SingleOrDefaultAsync(x =>
                x.ResetToken == token &&
                x.ResetTokenExpiryDate > DateTime.Now);
            if (dbUser != null)
            {
                return _mapper.Map<User>(dbUser);
            }
            return null;
        }

        public async Task<User> GetUserByRefreshToken(string token)
        {
            var dbUser = await _dbcontext.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            if (dbUser != null)
            {
                return _mapper.Map<User>(dbUser);
            }
            return null;
        }

    }
}
