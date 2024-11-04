using MassTransit;
using Microsoft.EntityFrameworkCore;
using VendorService.Consumers;
using VendorService.Data;
using VendorService.Events;
using VendorService.Repositories;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configure database context
builder.Services.AddDbContext<VendorContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("VendorDatabase")));

// Configure MassTransit with RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<LowStockConsumer>(); // Register the consumer

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("low_stock_alert_queue", e =>
        {
            e.ConfigureConsumer<LowStockConsumer>(context); // Bind the consumer to the queue
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
