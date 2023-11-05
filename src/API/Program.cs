using System.IdentityModel.Tokens.Jwt;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Repository.Models.Mapping;

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

builder.Services.ConfigureSqlContext(builder.Configuration);

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

app.Run();
