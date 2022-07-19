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
        public string RoleDescription { get; set; }       
        public string CreatedBy { get; set; }       
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
