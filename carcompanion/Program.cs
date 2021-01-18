using System;
using System.Threading;
using carcompanion.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace carcompanion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            
            try
            {
                Log.Information("Application starting up");
                
                var host = CreateHostBuilder(args).Build();

                ApplyMigrations(host);

                Log.Information("Application is ready to rock");
                
                host.Run();
                
                Log.Information("Application is shutting down");
            }
            catch (Exception e)
            {
                Log.Fatal(e, "The application failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        private static void ApplyMigrations(IHost host)
        {
            Log.Information("Trying to apply migrations into database");
            
            bool dbReadyToGo;
            do
            {
                try
                {
                    using (var scope = host.Services.CreateScope())
                    {
                        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        db.Database.Migrate();
                    }
                    dbReadyToGo = true;
                }
                catch (Exception)
                {
                    dbReadyToGo = false;
                    Log.Warning("Something went wrong while applying migrations");
                    Thread.Sleep(6000);
                    Log.Information("Trying again");
                }
                
            } while (dbReadyToGo == false);

            Log.Information("Migrations applied successfully");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
