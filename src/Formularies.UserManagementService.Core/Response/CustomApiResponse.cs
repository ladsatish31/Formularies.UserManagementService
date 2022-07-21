using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Response
{
    public class CustomApiResponse
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public object result { get; set; }       
        public Pagination pagination { get; set; }

        public CustomApiResponse(int statusCode=200, string message="", object result=null, Pagination pagination=null)
        {
            this.statusCode = statusCode;
            this.message = message == string.Empty ? "Success" : message; 
            this.result = result;
            this.pagination = pagination;    
        }
        public CustomApiResponse(object result = null,Pagination pagination = null)
        {
            this.statusCode = 200;
            this.message = "Success";
            this.result = result;            
            this.pagination = pagination;
        }

        public CustomApiResponse(object result)
        {
            this.statusCode = 200;
            this.result = result;
        }
    }

    public class Pagination
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }        
    }
}
