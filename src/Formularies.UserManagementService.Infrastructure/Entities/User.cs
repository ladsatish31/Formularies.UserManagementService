using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Infrastructure.Entities
{
    public class User
    {
        [Key]        
        public Guid UserId { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int RoleId { get; set; }        
        public string PasswordHash { get; set; }        
        public string ResetToken { get; set; }        
        public DateTime ResetTokenExpiryDate { get; set; }       
        public DateTime PasswordResetDate { get; set; }        
        public string CreatedBy { get; set; }       
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
