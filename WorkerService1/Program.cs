using DAL.Orders;
using DAL.Orders.Services;
using DAL.Orders.Services.Base;
using Microsoft.EntityFrameworkCore;
using Shared.Rabbit;
using WorkerService1;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();


//////
builder.Services.AddLogging();

var connectionString = (builder.Configuration.GetConnectionString("DefaultConnection") ??
                        builder.Configuration.GetConnectionString("Default")) ??
                        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));

// Бизнес-сервисы
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IOutboxMessagesService, OutboxMessagesService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();

// Rabbit
builder.Services.AddSingleton<IRabbitConsumer, RabbitConsumer>();
// Rabbit
builder.Services.AddSingleton<IRabbitConnection, RabbitConnection>();
builder.Services.AddSingleton<IRabbitPublisher, RabbitPublisher>();

// Worker
builder.Services.AddHostedService<OrdersConsumerWorker>();
builder.Services.AddHostedService<OutboxProcessorWorker>();

//////
///
var host = builder.Build();
host.Run();
