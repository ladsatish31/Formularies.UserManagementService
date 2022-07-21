using Formularies.UserManagementService.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Request
{
    public class UserUpdateRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int RoleId { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
        [JsonIgnore]
        public string ResetToken { get; set; }
        [JsonIgnore]
        public DateTime? ResetTokenExpiryDate { get; set; }
        [JsonIgnore]
        public DateTime? PasswordResetDate { get; set; }
    }
}
