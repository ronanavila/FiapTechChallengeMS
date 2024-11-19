using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TechChallenge.Infrastructure.Repository.ApplicationDbContext;

namespace TechChallange.IntegrationTests;
internal class ContactsWebApplictionFactory : WebApplicationFactory<Program>
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Tests");

    builder.ConfigureTestServices(services =>
    {
      services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
      var connString = GetConnectionString();
      services.AddSqlServer<ApplicationDbContext>(connString);
      var dbContext = CreateDbContext(services);
      dbContext.Database.EnsureDeleted();
      dbContext.Database.Migrate();
    });
  }

  private static string? GetConnectionString()
  {
    IConfiguration configuration =
          new ConfigurationBuilder()
          .AddJsonFile("appsettings.json")
          .Build();
    return configuration.GetConnectionString("IntegrationTestConnection");

  }

  private static ApplicationDbContext CreateDbContext(IServiceCollection services)
  {
    var serviceProvider = services.BuildServiceProvider();
    var scope = serviceProvider.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    return dbContext;
  }
}


