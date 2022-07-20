﻿using Formularies.UserManagementService.Core.Models;
using Formularies.UserManagementService.Core.Request;
using Formularies.UserManagementService.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(Guid id);
        Task<User> CreateUser(User user);
        Task<bool> DeleteUser(Guid id);
        Task<bool> UpdateUser(Guid id, User user);
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest, string ipAddress);
        Task<AuthenticateResponse> RefreshToken(string token, string ipAddress);
        Task ForgotPassword(ForgotPasswordRequest forgotPasswordRequest, string origin);
        Task ResetPassword(ResetPasswordRequest resetPasswordRequest);
    }
}
