using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
