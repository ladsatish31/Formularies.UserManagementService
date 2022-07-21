using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Core.Request
{
    public class SearchFilter
    {
        public string Search { get; set; }
        public string SortBy { get; set; }
       
        public SearchFilter()
        {          
            
        }
        public SearchFilter(string search, string sortBy)
        {
            this.Search = search;
            this.SortBy = sortBy;           
        }
    }
}
