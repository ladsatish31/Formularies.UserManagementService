using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;


namespace Formularies.UserManagementService.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json",
                    optional: false,
                    reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                    optional: true,
                    reloadOnChange: true)
                .AddUserSecrets<Startup>(optional: true, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.MySQL(
                    connectionString: configuration.GetConnectionString("DefaultConnection"))
                .CreateLogger();
            try
            {
                Log.Information("Starting the HostBuilder...");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The HostBuilder terminated unexpectedly");
                throw;
            }
            finally
            {
                Log.Information("HostBuilder is up and running.");
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        // Set properties and call methods on options
                    })
                    .UseStartup<Startup>();
                })
                .UseSerilog();
        }
    }
}
