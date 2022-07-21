using Formularies.UserManagementService.Core.Models;
using Formularies.UserManagementService.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        //Task<IEnumerable<User>> GetAllUsers();
        IQueryable<User> GetAllUsers();        
        Task<User> GetUserById(Guid id);
        Task<UserCreateRequest> CreateUser(UserCreateRequest user);
        Task<bool> DeleteUser(Guid id);
        Task<bool> UpdateUser(Guid id, UserUpdateRequest user);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByResetToken(string token);
        Task<User> GetUserByRefreshToken(string token);
        //IQueryable<User> GetUsers();
    }
}
