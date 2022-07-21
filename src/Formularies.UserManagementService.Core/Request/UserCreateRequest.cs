using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Request
{
    public class UserCreateRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }= Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
