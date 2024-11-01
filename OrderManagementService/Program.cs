using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderManagementService.Data;
using OrderManagementService.Repositories;
using OrderManagementService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("OrderManagementDatabase")));

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq");
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

