using Formularies.UserManagementService.Api.Middlewares;
using Formularies.UserManagementService.Core.Interfaces.Repositories;
using Formularies.UserManagementService.Core.Interfaces.Services;
using Formularies.UserManagementService.Core.Services;
using Formularies.UserManagementService.Infrastructure.Context;
using Formularies.UserManagementService.Infrastructure.Respositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft;
using Formularies.UserManagementService.Core.Constants;
using System.Text;

namespace Formularies.UserManagementService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            //services.AddControllers().AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            //});
            services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.ConfigureCors();
            services.ConfigureSwagger();
            services.ConfigureDependencyInjection(Configuration);
            services.AddAutoMapper(typeof(Startup));
            services.AddRouting(option=>option.LowercaseUrls=true);
            var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.HttpCodeAndLogMiddleware();
            }
            else
            {
                app.HttpCodeAndLogMiddleware();
                app.UseHsts();
            }
            app.ConfigureSwagger(provider);
            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseAuthorization();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
