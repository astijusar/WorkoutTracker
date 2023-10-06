using API.Extensions;
using API.Models.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.ConfigureRepositoryManger();
builder.Services.ConfigureFilters();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.ConfigureControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
