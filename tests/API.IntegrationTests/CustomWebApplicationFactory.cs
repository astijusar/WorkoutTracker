using Data.Repository;
using Data.Repository.Seeders;
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

                var connectionString = "Host=localhost;Port=5432;Database=WorkoutTrackerDb;Username=postgres;Password=password";

                services.AddDbContext<ApplicationContext>(options =>
                {
                    options.UseNpgsql(connectionString);
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
