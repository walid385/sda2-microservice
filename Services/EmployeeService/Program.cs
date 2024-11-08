using EmployeeService.Data;
using EmployeeService.Repositories;
using EmployeeService.Consumers;
using EmployeeService.Profiles;
using EmployeeService.Services;
using Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("EmployeeDatabase")));

// Register the repository
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddAutoMapper(typeof(EmployeeProfile));


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

builder.Services.AddHttpClient<CustomerClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5000"); 
});



// Add controllers and Swagger configuration
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
