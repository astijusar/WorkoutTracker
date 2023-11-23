using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = "Host=localhost;Port=5432;Database=WorkoutTrackerDb;Username=postgres;Password=password";

            services.AddDbContext<ApplicationContext>(opt =>
            {
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                {
                    var m = Regex.Match(Environment.GetEnvironmentVariable("DATABASE_URL")!, @"postgres://(.*):(.*)@(.*):(.*)/(.*)");
                    opt.UseNpgsql($"Server={m.Groups[3]};Port={m.Groups[4]};User Id={m.Groups[1]};Password={m.Groups[2]};Database={m.Groups[5]};sslmode=Prefer;Trust Server Certificate=true");
                }
                else // In Development Environment
                {
                    // So, use a local Connection
                    opt.UseNpgsql(connectionString!);
                }
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
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                {
                    opt.TokenValidationParameters.ValidAudience = Environment.GetEnvironmentVariable("ValidAudience");
                    opt.TokenValidationParameters.ValidIssuer = Environment.GetEnvironmentVariable("ValidIssuer");
                    opt.TokenValidationParameters.IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Secret")!));
                }
                else
                {
                    opt.TokenValidationParameters.ValidAudience = configuration["Jwt:ValidAudience"];
                    opt.TokenValidationParameters.ValidIssuer = configuration["Jwt:ValidIssuer"];
                    opt.TokenValidationParameters.IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!));
                }
            });

            services.AddAuthorization();
        }

        public static void ConfigureJwtOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(opt =>
            {
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                {
                    opt.Secret = Environment.GetEnvironmentVariable("Secret")!;
                    opt.ValidAudience = Environment.GetEnvironmentVariable("ValidAudience")!;
                    opt.ValidIssuer = Environment.GetEnvironmentVariable("ValidIssuer")!;
                }
                else
                {
                    opt.Secret = configuration["Jwt:Secret"]!;
                    opt.ValidAudience = configuration["Jwt:ValidAudience"]!;
                    opt.ValidIssuer = configuration["Jwt:ValidIssuer"]!;
                }
            });
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        public static void ConfigureUserPasswordOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<UserPasswordOptions>(opt =>
            {
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                {
                    opt.AdminPassword = Environment.GetEnvironmentVariable("AdminPassword")!;
                    opt.DemoUserPassword = Environment.GetEnvironmentVariable("DemoUserPassword")!;
                }
                else
                {
                    opt.AdminPassword = configuration["AdminPassword"]!;
                    opt.DemoUserPassword = configuration["DemoUserPassword"]!;
                }
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
