using Formularies.UserManagementService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Formularies.UserManagementService.Infrastructure.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option):base(option)
        {
            
        }
        public virtual DbSet<Role> Roles { get; set; }

        //private void SeedData()
        //{
        //    var roles = new List<Role>()
        //    {
        //        new Role() { RoleId = 1,RoleName = "Super_Admin",RoleDescription="Super Admin", CreatedBy="lad.satish1@gmail.com"},
        //        new Role() { RoleId = 2,RoleName = "Admin",RoleDescription="Admin", CreatedBy="lad.satish1@gmail.com"},
        //        new Role() { RoleId = 3,RoleName = "Analyst",RoleDescription="Analyst", CreatedBy="lad.satish1@gmail.com"},
        //        new Role() { RoleId = 4,RoleName = "Reviewer",RoleDescription="Reviewer", CreatedBy="lad.satish1@gmail.com"}
        //    };
        //    Roles.AddRange(roles);
        //    SaveChanges();
        //}
    }
}
