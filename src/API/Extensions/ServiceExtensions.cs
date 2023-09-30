﻿using System.Text.Json.Serialization;
using API.Models;
using API.Repository;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                .AddJsonOptions(opt =>
                {
                    var enumConverter = new JsonStringEnumConverter();
                    opt.JsonSerializerOptions.Converters.Add(enumConverter);
                });
        }
    }
}
