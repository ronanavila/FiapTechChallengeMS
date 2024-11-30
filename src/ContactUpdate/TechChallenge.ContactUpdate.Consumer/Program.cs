using MassTransit;
using Microsoft.EntityFrameworkCore;
using TechChallenge.ContactUpdate.Consumer;
using TechChallenge.ContactUpdate.Consumer.Events;
using TechChallenge.ContactUpdate.Consumer.Services;
using TechChallenge.Domain.Repository;
using TechChallenge.Infrastructure.Repository;
using TechChallenge.Infrastructure.Repository.ApplicationDbContext;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var configuration = builder.Configuration;
var queueName = configuration.GetSection("MassTransit")["QueueName"] ?? string.Empty;
var server = configuration.GetSection("MassTransit")["Server"] ?? string.Empty;
var user = configuration.GetSection("MassTransit")["User"] ?? string.Empty;
var password = configuration.GetSection("MassTransit")["Password"] ?? string.Empty;
var connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddTransient<IUpdateContactService, UpdateContactService>();
builder.Services.AddTransient<IContactRepository, ContactRepository>();

builder.Services.AddMassTransit(x =>
{
  x.AddConsumer<ContactUpdateConsumer>();

  x.UsingRabbitMq((context, config) =>
  {
    config.Host(server, h =>
    {
      h.Username(user);
      h.Password(password);
    });

    config.ConfigureEndpoints(context);

    config.ReceiveEndpoint(queueName, ep =>
    {
      ep.ConfigureConsumer<ContactUpdateConsumer>(context);
      ep.Bind("Update");
    });
  });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  options.UseSqlServer(connectionString);
  options.UseLazyLoadingProxies();
}, ServiceLifetime.Scoped);

var host = builder.Build();
host.Run();
