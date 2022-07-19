using Formularies.UserManagementService.Core.Interfaces.Repositories;
using Formularies.UserManagementService.Core.Interfaces.Services;
using Formularies.UserManagementService.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        public readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<User> CreateUser(User user)
        {
            try
            {
                return await _userRepository.CreateUser(user);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call CreateUser in service class, Error message={ex}.");
                throw;
            }
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            try
            {
                return await _userRepository.DeleteUser(id);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call DeleteUser in service class, Error message={ex}.");
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await _userRepository.GetAllUsers();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call GetAllUsers in service class, Error message={ex}.");
                throw;
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                return await _userRepository.GetUserById(id);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call GetUserById in service class, Error message={ex}.");
                throw;
            }
        }

        public async Task<bool> UpdateUser(Guid id, User user)
        {
            try
            {
                return await _userRepository.UpdateUser(id, user);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call UpdateUser in service class, Error message={ex}.");
                throw;
            }
        }
    }
}
