using MassTransit;
using Microsoft.EntityFrameworkCore;
using SalesService.Data;
using SalesService.Repositories;
using SalesService.Consumers;
using SalesService.Services;
using Events;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext for SalesService
builder.Services.AddDbContext<SalesContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SalesDatabase")));

// Configure MassTransit and RabbitMQ
// SalesService Program.cs
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

        cfg.Message<OrderCreatedEvent>(e =>
        {
            e.SetEntityName("Events:OrderCreatedEvent");
        });

        cfg.Publish<OrderCreatedEvent>(e =>
        {
            e.ExchangeType = "direct";
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



// Register dependencies without an interface for InventoryClient
builder.Services.AddHttpClient<InventoryClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5001");
});

builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
