using System.Reflection;
using System.Text.Json.Serialization;
using API.Filters;
using API.Models;
using API.Repository;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:Database"];

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
            services.AddScoped<WorkoutExistsFilterAttribute>();
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
    }
}
