using CustomerService.Data;
using CustomerService.Repositories;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using CustomerService.Consumers;
using CustomerService.Services;


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
    x.AddConsumer<UpdateCustomerRewardsConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq");
        cfg.ReceiveEndpoint("update-customer-rewards-queue", e =>
        {
            e.ConfigureConsumer<UpdateCustomerRewardsConsumer>(context);
        });
    });
});

// Register dependencies without an interface for OrderManagementService
builder.Services.AddHttpClient<OrderManagementClient>(client =>
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
