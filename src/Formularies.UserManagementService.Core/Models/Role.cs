using System;
using System.ComponentModel.DataAnnotations;

namespace Formularies.UserManagementService.Core.Models
{
    public class Role
    {
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }        

    }
}
