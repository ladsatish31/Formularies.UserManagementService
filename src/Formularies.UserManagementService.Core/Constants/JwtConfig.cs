using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Constants
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public TimeSpan TokenExpiryTime { get; set; }
        public int RefreshTokenTTL { get; set; }
        public int RefreshTokenExpiryTime { get; set; }
        public int ResetTokenExpiryTime { get; set; }

    }
}
