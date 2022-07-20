using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Models
{
    public class User
    {
        public Guid UserId { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
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
