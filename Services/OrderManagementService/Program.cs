using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderManagementService.Data;
using OrderManagementService.Repositories;
using OrderManagementService.Services;
using Events;
using OrderManagementService.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("OrderManagementDatabase")));

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
            e.SetEntityName("Events:OrderCreatedEvent"); // Same exchange name
        });

        cfg.Publish<OrderCreatedEvent>(e =>
        {
            e.ExchangeType = "direct";
        });

        cfg.ReceiveEndpoint("order-management-order-created-queue", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(context);

            e.Bind("Events:OrderCreatedEvent", s =>
            {
                s.RoutingKey = "orderCreated"; // Same routing key
                s.ExchangeType = "direct";
            });
        });
    });
});


builder.Services.AddHttpClient<InventoryClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5001");
});


builder.Services.AddScoped<IOrderRepository, OrderRepository>();
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

