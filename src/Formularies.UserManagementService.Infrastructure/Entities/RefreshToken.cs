using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Infrastructure.Entities
{
    [Owned]
    public class RefreshToken
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public string Token { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }        
        public string CreatedByIp { get; set; }
        public bool IsExpired => DateTime.Now >= ExpiryDate;
    }
}
