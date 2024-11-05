using CustomerService.Data;
using CustomerService.Repositories;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using CustomerService.Consumers;
using CustomerService.Services;
using Events;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<CustomerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CustomerDatabase")));

// Add MassTransit for RabbitMQ
builder.Services.AddMassTransit(x =>
{
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
    });
});

builder.Services.AddHttpClient<InventoryClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5001");
});


// Register repositories for Dependency Injection
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IGiftCardRepository, GiftCardRepository>();
builder.Services.AddScoped<IReturnTableRepository, ReturnTableRepository>();
builder.Services.AddScoped<IItemListRepository, ItemListRepository>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

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
