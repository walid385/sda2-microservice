using EmployeeService.Data;
using EmployeeService.Repositories;
using EmployeeService.Consumers;
using Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("EmployeeDatabase")));

// Register the repository
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Register MassTransit with RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.Publish<OrderCreatedEvent>(e =>
        {
            e.ExchangeType = "direct";  // Ensure consistency
        });

        cfg.ReceiveEndpoint("order-created-queue", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(context);
            e.Bind("Events:OrderCreatedEvent", s =>
            {
                s.RoutingKey = "orderCreated";
                s.ExchangeType = "direct";
            });
        });
    });
});


// Add controllers and Swagger configuration
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Servi