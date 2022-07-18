using System;
using System.ComponentModel.DataAnnotations;

namespace Formularies.UserManagementService.Infrastructure.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string RoleDescription { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

    }
}
