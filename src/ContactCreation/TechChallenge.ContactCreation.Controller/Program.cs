using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using System.Reflection;
using TechChallenge.ContactCreation.Application.Services;
using TechChallenge.Infrastructure.Repository.ApplicationDbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
  options =>
  {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
      Version = "v1",
      Title = "Contact Api",
      Description = "Api for maintain contacts",


    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
  });


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

  var connectionString = configuration.GetConnectionString("DefaultConnection");
  options.UseSqlServer(connectionString);
  options.UseLazyLoadingProxies();
}, ServiceLifetime.Scoped);

builder.Services.AddTransient<IContactService, ContactService>();

builder.Services.AddOpenTelemetry()
  .WithMetrics(x =>
  {
    x.AddRuntimeInstrumentation()
      .AddMeter(
        "Microsoft.AspNetCore.Hosting",
        "Microsoft.AspNetCore.Server.Kestrel",
        "System.Net.Http"
       )
      .AddPrometheusExporter();
  })
  .WithTracing(x =>
  {
    x.AddAspNetCoreInstrumentation()
      .AddHttpClientInstrumentation();
  }
);

var configuration = builder.Configuration;
var queueName = configuration.GetSection("MassTransit")["QueueName"] ?? string.Empty;
var server = configuration.GetSection("MassTransit")["Server"] ?? string.Empty;
var user = configuration.GetSection("MassTransit")["User"] ?? string.Empty;
var password = configuration.GetSection("MassTransit")["Password"] ?? string.Empty;


builder.Services.AddMassTransit(x =>
{
  x.UsingRabbitMq((context, cfg) =>
  {
    cfg.Host(server, "/", h =>
    {
      h.Username(user);
      h.Password(password);
    });

    cfg.ConfigureEndpoints(context);
  });
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapPrometheusScrapingEndpoint();
app.MapControllers();

app.Run();
