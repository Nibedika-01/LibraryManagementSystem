using AutoMapper;
using LibraryManagementSystem.Application.Mapping;
using LibraryManagementSystem.Application.Services;
using LibraryManagementSystem.Infrastructure.Persistence;
using LibraryManagementSystem.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddAutoMapper(typeof(MappingProfile)); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library Management API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Management API v1"));
};

app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    context.Database.Migrate();
}

app.Run();