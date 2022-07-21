using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Request
{
    public class RoleRequest
    {
        [JsonIgnore]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
}
