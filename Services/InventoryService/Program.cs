using Microsoft.EntityFrameworkCore;
using InventoryService.Data;
using InventoryService.Repositories;
using Events;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<InventoryContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("InventoryDatabase")));

builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();

// Configure MassTransit with RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // Configure the direct exchange for publishing ILowStockEvent
        cfg.Message<ILowStockEvent>(e =>
        {
            e.SetEntityName("InventoryService.Events:LowStockEvent");
        });

        cfg.Publish<ILowStockEvent>(e =>
        {
            e.ExchangeType = "direct";
        });
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
