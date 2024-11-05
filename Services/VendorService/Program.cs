using MassTransit;
using Microsoft.EntityFrameworkCore;
using VendorService.Consumers;
using VendorService.Data;
using Events;
using VendorService.Repositories;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configure database context
builder.Services.AddDbContext<VendorContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("VendorDatabase")));

// Configure MassTransit with RabbitMQ
builder.Services.AddMassTransit(x =>
{
    // Register the consumer
    x.AddConsumer<LowStockConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // Configure the exchange for publishing
        cfg.Message<ILowStockEvent>(e =>
        {
            e.SetEntityName("InventoryService.Events:LowStockEvent");
        });

        cfg.Publish<ILowStockEvent>(e =>
        {
            e.ExchangeType = "direct"; // Set the exchange type to direct
        });

        // Configure the consumer to listen to a direct exchange with routing key
        cfg.ReceiveEndpoint("low_stock_alert_queue", e =>
        {
            e.ConfigureConsumer<LowStockConsumer>(context);

            e.Bind("InventoryService.Events:LowStockEvent", s =>
            {
                s.RoutingKey = "low_stock";
                s.ExchangeType = "direct";
            });
        });
    });
});

// Dependency injection for repository
builder.Services.AddScoped<IVendorRepository, VendorRepository>();

// Add controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure middleware
app.UseAuthorization();
app.MapControllers();
app.Run();
