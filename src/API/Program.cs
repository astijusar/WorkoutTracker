using System.IdentityModel.Tokens.Jwt;
using API.Extensions;
using API.Models.Mapping;
using API.Repository.Seeders;
using API.Services;
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

builder.Services.ConfigureRepositoryManger();
builder.Services.ConfigureFilters();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.ConfigureControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureAuthenticationAndAuthorization(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.SeedDatabase();

app.Run();
