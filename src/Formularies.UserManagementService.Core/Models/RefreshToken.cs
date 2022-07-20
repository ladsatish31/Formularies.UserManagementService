using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }        
        public Guid UserId { get; set; }
        public string Token { get; set; }      
        public DateTime ExpiryDate { get; set; }       
        public DateTime CreatedDate { get; set; }
        public string CreatedByIp { get; set; }
        public bool IsExpired => DateTime.Now >= ExpiryDate;
    }
}
