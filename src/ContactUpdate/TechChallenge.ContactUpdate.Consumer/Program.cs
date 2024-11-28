using MassTransit;
using TechChallenge.ContactUpdate.Consumer;
using TechChallenge.ContactUpdate.Consumer.Events;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var configuration = builder.Configuration;
var queueName = configuration.GetSection("MassTransit")["QueueName"] ?? string.Empty;
var server = configuration.GetSection("MassTransit")["Server"] ?? string.Empty;
var user = configuration.GetSection("MassTransit")["User"] ?? string.Empty;
var password = configuration.GetSection("MassTransit")["Password"] ?? string.Empty;

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

var host = builder.Build();
host.Run();
