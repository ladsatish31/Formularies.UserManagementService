﻿using System;

namespace Formularies.UserManagementService.Core.Models
{
    public class Role
    {
        public int RoleId { get; set; }       
        public string RoleName { get; set; }       
        public string RoleDescription { get; set; }
        public string CreatedBy { get; set; }       
        public DateTime CreatedDate { get; set; }

    }
}
