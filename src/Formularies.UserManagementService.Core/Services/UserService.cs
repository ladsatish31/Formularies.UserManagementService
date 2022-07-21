using AutoMapper;
using Formularies.UserManagementService.Core.Constants;
using Formularies.UserManagementService.Core.Interfaces.Repositories;
using Formularies.UserManagementService.Core.Interfaces.Services;
using Formularies.UserManagementService.Core.Models;
using Formularies.UserManagementService.Core.Request;
using Formularies.UserManagementService.Core.Response;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using AutoWrapper.Wrappers;

namespace Formularies.UserManagementService.Core.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        public readonly ILogger<UserService> _logger;
        private readonly JwtConfig _jwtConfig;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IOptionsMonitor<JwtConfig> optionsMonitor, IMapper mapper, IEmailService emailService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jwtConfig = optionsMonitor.CurrentValue ?? throw new ArgumentNullException(nameof(optionsMonitor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }
        public async Task<UserCreateRequest> CreateUser(UserCreateRequest user)
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

        //public async Task<IEnumerable<User>> GetAllUsers()
        //{
        //    try
        //    {
        //        return await _userRepository.GetAllUsers();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        _logger.LogError($"Error while trying to call GetAllUsers in service class, Error message={ex}.");
        //        throw;
        //    }
        //}
        public IEnumerable<User> GetAllUsers(SearchFilter searchFilter)
        {
            try
            {
                var allUsers = _userRepository.GetAllUsers();

                if (!string.IsNullOrEmpty(searchFilter.Search))
                {
                    allUsers = allUsers.Where(r => r.Email.Contains(searchFilter.Search) || r.Name.Contains(searchFilter.Search));
                }
                
                //Default sort by Name
                allUsers = allUsers.OrderBy(user => user.Name);
                if (!string.IsNullOrEmpty(searchFilter.SortBy))
                {
                    switch (searchFilter.SortBy)
                    {
                        case "name_asc": allUsers = allUsers.OrderBy(hh => hh.Name); break;
                        case "name_desc": allUsers = allUsers.OrderByDescending(hh => hh.Name); break;
                        case "email_asc": allUsers = allUsers.OrderBy(hh => hh.Email); break;
                        case "email_desc": allUsers = allUsers.OrderByDescending(hh => hh.Email); break;
                        case "role_asc": allUsers = allUsers.OrderBy(hh => hh.RoleId); break;
                        case "role_desc": allUsers = allUsers.OrderByDescending(hh => hh.RoleId); break;
                        case "status_asc": allUsers = allUsers.OrderBy(hh => hh.IsActive); break;
                        case "staus_desc": allUsers = allUsers.OrderByDescending(hh => hh.IsActive); break;
                    }
                }
                return allUsers.Select(user => new User
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    RoleId = user.RoleId,                  
                    IsActive = user.IsActive                    
                }).ToList();
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

        public async Task<bool> UpdateUser(Guid id, UserUpdateRequest user)
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

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest, string ipAddress)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(authenticateRequest.Email);
                if (user == null)
                    throw new ApiException("Email does not exist",404);
                else if (!user.IsActive)
                    throw new ApiException("User is not activated",400);
                if (!BC.Verify(authenticateRequest.Password, user.PasswordHash))
                    throw new ApiException("Email or password is incorrect",400);

                // authentication successful so generate jwt and refresh tokens
                var jwtToken = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken(ipAddress);
                user.RefreshTokens.Add(refreshToken);

                // remove old refresh tokens from user
                RemoveOldRefreshTokens(user);

                // save changes to db
                await _userRepository.UpdateUser(user.UserId, _mapper.Map<UserUpdateRequest>(user));

                var response = _mapper.Map<AuthenticateResponse>(user);
                response.JwtToken = jwtToken;
                response.RefreshToken = refreshToken.Token;
                return response;
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call Authenticate in user service class, Error message={ex}.");
                throw;// new ApiException(ex.ToString());
            }
        }

        public async Task ForgotPassword(ForgotPasswordRequest forgotPasswordRequest, string origin)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(forgotPasswordRequest.Email);

                // always return ok response to prevent email enumeration
                if (user == null) throw new ApiException("Email not valid",404);

                // create reset token that expires after 1 day
                user.ResetToken = RandomTokenString();
                user.ResetTokenExpiryDate = DateTime.Now.AddDays(_jwtConfig.ResetTokenExpiryTime);

                await _userRepository.UpdateUser(user.UserId, _mapper.Map<UserUpdateRequest>(user));

                // send email
                //SendPasswordResetEmail(user, origin);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call Authenticate in user service class, Error message={ex}.");
                throw;
            }
        }
        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            try
            {
                var (refreshToken, user) = await GetRefreshToken(token);

                // replace old refresh token with a new one and save
                var newRefreshToken = GenerateRefreshToken(ipAddress);
                user.RefreshTokens.Add(newRefreshToken);

                RemoveOldRefreshTokens(user);

                await _userRepository.UpdateUser(user.UserId, _mapper.Map<UserUpdateRequest>(user));

                // generate new jwt
                var jwtToken = GenerateJwtToken(user);

                var response = _mapper.Map<AuthenticateResponse>(user);
                response.JwtToken = jwtToken;
                response.RefreshToken = newRefreshToken.Token;
                return response;
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call RefreshToken in user service class, Error message={ex}.");
                throw;
            }
        }

        public async Task ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                var user = await _userRepository.GetUserByResetToken(resetPasswordRequest.Token);

                if (user == null)
                    throw new ApiException("Invalid token", 404);

                // update password and remove reset token
                user.PasswordHash = BC.HashPassword(resetPasswordRequest.Password);
                user.PasswordResetDate = DateTime.Now;
                user.ResetToken = null;
                user.ResetTokenExpiryDate = null;
                await _userRepository.UpdateUser(user.UserId, _mapper.Map<UserUpdateRequest>(user));
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call ResetPassword in user service class, Error message={ex}.");
                throw;
            }
        }     

        
        private async Task<(RefreshToken, User)> GetRefreshToken(string token)
        {
            try
            {
                var user = await _userRepository.GetUserByRefreshToken(token);
                if (user == null) throw new ApiException("Invalid token", 404 );
                var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
                if (refreshToken.IsExpired) throw new ApiException("Invalid token", 404);
                return (refreshToken, user);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call GetRefreshToken in user service class, Error message={ex}.");
                throw;
            }
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                Expires = DateTime.Now.Add(_jwtConfig.TokenExpiryTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                ExpiryDate = DateTime.Now.AddDays(_jwtConfig.RefreshTokenExpiryTime),
                CreatedDate = DateTime.Now,
                CreatedByIp = ipAddress
            };
        }

        private void RemoveOldRefreshTokens(User user)
        {
            user.RefreshTokens.RemoveAll(x =>
                x.IsExpired &&
                x.CreatedDate.AddDays(_jwtConfig.RefreshTokenTTL) <= DateTime.Now);
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private void SendPasswordResetEmail(User user, string origin)
        {
            try
            {
                string message;
                if (!string.IsNullOrEmpty(origin))
                {
                    var resetUrl = $"{origin}/api/login/resetpassword?token={user.ResetToken}";
                    message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
                }
                else
                {
                    message = $@"<p>Please use the below token to reset your password with the <code>/api/login/resetpassword</code> api route:</p>
                             <p><code>{user.ResetToken}</code></p>";
                }

                _emailService.Send(
                    to: user.Email,
                    subject: "Reset Password",
                    html: $@"<h4>Reset Password Email</h4>
                         {message}"
                );
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call SendPasswordResetEmail in user service class, Error message={ex}.");
                throw;
            }

        }
    }
}
