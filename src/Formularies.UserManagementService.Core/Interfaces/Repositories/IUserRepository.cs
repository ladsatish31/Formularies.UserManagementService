using Formularies.UserManagementService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(Guid id);
        Task<User> CreateUser(User user);
        Task<bool> DeleteUser(Guid id);
        Task<bool> UpdateUser(Guid id, User user);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByResetToken(string token);
    }
}
