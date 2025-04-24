using CrudTaskAPI.Application.Interfaces;
using CrudTaskAPI.Application.Services;
using CrudTaskAPI.Domain.Interfaces;
using CrudTaskAPI.Infra.Data.Repositories;
using CrudTaskAPI.Infra.Infrastructure.Data;
using CrudTaskAPI.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenLocalhost(5001);
});

builder.Services.AddDbContext<ConnectionContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CrudTaskAPI",
        Version = "v1",
        Description = "API para gerenciamento de tarefas",
        Contact = new OpenApiContact
        {
            Name = "Guilherme Rudio",
            Email = "guilhermerudio6@gmail.com"
        }
    });
});

builder.Services.AddScoped<IChoreService, ChoreService>();
builder.Services.AddScoped<IChoreRepository, ChoreRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CrudTaskAPI v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
