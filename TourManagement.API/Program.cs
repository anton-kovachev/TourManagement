using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TourManagement.API.Services;

namespace TourManagement.API
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using(var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<TourManagementContext>();
                context.Database.Migrate();
                context.EnsureSeedDatabase();
            }

            host.Run();
        }

        private static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                        .UseStartup<TourManagement.API.Startup>()
                        .Build();
        }
    }
}
