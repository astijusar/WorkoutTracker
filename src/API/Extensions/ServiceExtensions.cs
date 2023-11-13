using System.Reflection;
using System.Text;
using API.Filters;
using Core;
using Core.Models;
using Core.Options;
using Data.Options;
using Data.Repository;
using Data.Repository.Interfaces;
using Data.Repository.Seeders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration["ConnectionStrings:Database"];
            var connectionString = "Server=127.0.0.1\\mssql,1433;Database=WorkoutTrackerDB;User=sa;Password=/Password12;TrustServerCertificate=Yes";

            services.AddDbContext<ApplicationContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
                opt.EnableDetailedErrors();
            });
        }

        public static void ConfigureRepositoryManger(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(opt => opt
                    .SerializerSettings.Converters.Add(new StringEnumConverter()));
        }

        public static void ConfigureFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<ExerciseExistsFilterAttribute>();
            services.AddScoped<WorkoutForUserExistsFilterAttribute>();
            services.AddScoped<WorkoutExerciseExistsFilterAttribute>();
            services.AddScoped<UserExistsFilterAttribute>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigureAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters.ValidAudience = configuration["Jwt:ValidAudience"];
                opt.TokenValidationParameters.ValidIssuer = configuration["Jwt:ValidIssuer"];
                opt.TokenValidationParameters.IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!));
            });

            services.AddAuthorization();
        }

        public static void ConfigureJwtOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(opt =>
            {
                opt.Secret = configuration["Jwt:Secret"]!;
                opt.ValidAudience = configuration["Jwt:ValidAudience"]!;
                opt.ValidIssuer = configuration["Jwt:ValidIssuer"]!;
            });
        }

        public static void ConfigureUserPasswordOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<UserPasswordOptions>(opt =>
            {
                opt.AdminPassword = configuration["AdminPassword"]!;
                opt.DemoUserPassword = configuration["DemoUserPassword"]!;
            });
        }

        public static async Task SeedDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var authDbSeeder = scope.ServiceProvider.GetRequiredService<AuthDbSeeder>();
            var dbSeeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();

            await authDbSeeder.SeedAsync();
            await dbSeeder.SeedAsync();
        }

        public static void RunMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            dbContext.Database.Migrate();
        }
    }
}
