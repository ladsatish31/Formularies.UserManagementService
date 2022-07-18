using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            if (currentEnvironment?.Equals("Development", StringComparison.OrdinalIgnoreCase) == true)
            {
                configurationBuilder.AddJsonFile($"appsettings.{currentEnvironment}.json", optional: false);
            }
            try
            {
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //logManager.Shutdown();
            }
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
