using Microsoft.EntityFrameworkCore;
using SalesService.Data;
using SalesService.Repositories;
using MassTransit;
using SalesService.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SalesContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SalesDatabase")));

// MassTransit with RabbitMQ for inter-service communication
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq");
    });
});

builder.Services.AddHttpClient<InventoryClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5001"); // Replace 5001 with the actual port if different
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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
