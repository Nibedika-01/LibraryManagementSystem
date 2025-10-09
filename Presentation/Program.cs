using LibraryManagementSystem.Application.Services;
using LibraryManagementSystem.Infrastructure.Persistence;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LibraryManagementSystem.Presentation;

class Program
{
    static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<LibraryDbContext>();
        await context.Database.MigrateAsync();

        var app = services.GetRequiredService<LibraryApp>();
        await app.RunAsync();
    }
    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false);
        }).ConfigureServices((context, services) =>
        {
            services.AddDbContext<LibraryDbContext>(options =>
                    options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<ILibraryService, LibraryService>();

            services.AddScoped<LibraryApp>();
        });
}

