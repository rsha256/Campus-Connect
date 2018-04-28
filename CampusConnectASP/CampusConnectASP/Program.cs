using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;
using System.Text;

namespace CampusConnect
{
    public class Program
    {
        public static void Main(string[] args) =>
            BuildWebHost(args).EnsureDatabaseCreated().Run();

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }

    static class EnsureCreated
    {
        // Ensure database is created
        public static IWebHost EnsureDatabaseCreated(this IWebHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory)webHost.Services.GetService(typeof(IServiceScopeFactory));
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<UserContext>();

                dbContext.Database.EnsureCreated();
            }

            return webHost;
        }
    }
}