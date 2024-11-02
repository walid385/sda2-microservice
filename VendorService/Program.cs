using MassTransit;
using Microsoft.EntityFrameworkCore;
using VendorService.Models;
using VendorService.Data;
using VendorService.Repositories;
using AutoMapper;
using VendorService.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Configure database context
builder.Services.AddDbContext<VendorContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("VendorDatabase")));

builder.Services.AddMassTransit(x =>
{

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq");
        
    });
});

// Configure dependency injection
builder.Services.AddScoped<IVendorRepository, VendorRepository>();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

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
