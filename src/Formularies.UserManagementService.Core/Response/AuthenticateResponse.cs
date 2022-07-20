using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Response
{
    public class AuthenticateResponse
    {       
        public string Name { get; set; }
        public string Email { get; set; }       
        public string JwtToken { get; set; }
        //[JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

    }
}
