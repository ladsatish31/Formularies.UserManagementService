using System.Collections.Generic;
using System.Linq;

namespace Formularies.UserManagementService.Core.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, int statusCode = 200, string message = "",string version="")
        {
            this.statusCode = statusCode;
            this.message = message == string.Empty ? "Get request successful" : message;
            this.errors = null;
            this.result = data;
            this.version = version== string.Empty ? "1.0.0.0":version;
        }
        public T result { get; set; }    
        public string[] errors { get; set; }        
        public int statusCode { get; set; }
        public string message { get; set; }
        public string version { get; set; }
         
    }
}
