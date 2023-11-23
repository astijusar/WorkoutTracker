using System.IdentityModel.Tokens.Jwt;
using API.Extensions;
using Core.Models.Mapping;
using Core.Services;
using Data.Repository.Seeders;
using Microsoft.AspNetCore.Mvc;

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddTransient<JwtTokenService>();
builder.Services.AddScoped<AuthDbSeeder>();
builder.Services.AddScoped<DbSeeder>();

builder.Services.ConfigureRepositoryManger();
builder.Services.ConfigureFilters();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.ConfigureControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureCors();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureAuthenticationAndAuthorization(builder.Configuration);
builder.Services.ConfigureJwtOptions(builder.Configuration);
builder.Services.ConfigureUserPasswordOptions(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
    //app.RunMigrations();
    await app.SeedDatabase();
}
catch (Exception e)
{
    app.Logger.LogError($"Database Error: {e.Message}");
}

app.Run();

public partial class Program { }