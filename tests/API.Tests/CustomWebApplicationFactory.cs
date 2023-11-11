using API.Repository;
using API.Repository.Seeders;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.IntegrationTests
{
    public class CustomWebApplicationFactory<TProgram> 
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationContext>));

                if (descriptor != null)
                    services.Remove(descriptor);


                services.AddDbContext<ApplicationContext>(options =>
                {
                    options.UseSqlServer("Server=127.0.0.1\\mssqltest,1433;Database=WorkoutTrackerDB;User=sa;Password=/Password12;TrustServerCertificate=Yes");
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                var authDbSeeder = scope.ServiceProvider.GetRequiredService<AuthDbSeeder>();
                var dbSeeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();

                appContext.Database.EnsureCreated();
                authDbSeeder.SeedAsync().Wait();
                dbSeeder.SeedAsync().Wait();
            });
        }
    }
}
