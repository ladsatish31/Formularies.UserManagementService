using Formularies.UserManagementService.Core.Interfaces.Repositories;
using Formularies.UserManagementService.Core.Interfaces.Services;
using Formularies.UserManagementService.Core.Services;
using Formularies.UserManagementService.Infrastructure.Context;
using Formularies.UserManagementService.Infrastructure.Respositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Formularies.UserManagementService.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services,IConfiguration configuration)
        {
            if(services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }            
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            
            return services;
        }
    }
}
