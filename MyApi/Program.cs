using DAL.Orders;
using DAL.Orders.Services;
using DAL.Orders.Services.Base;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Shared.Rabbit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddLogging();

var connectionString = (builder.Configuration.GetConnectionString("DefaultConnection") ??
                        builder.Configuration.GetConnectionString("Default")) ??
                        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));

// Бизнес-сервисы
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddScoped<IOutboxMessagesService, OutboxMessagesService>();

// Rabbit
builder.Services.AddSingleton<IRabbitConsumer, RabbitConsumer>();
builder.Services.AddSingleton<IRabbitConnection, RabbitConnection>();
builder.Services.AddSingleton<IRabbitPublisher, RabbitPublisher>();

// Worker
//builder.Services.AddHostedService<OrdersConsumerWorker>();


await builder.Build().RunAsync();

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Service", "OrdersWorker")
    .WriteTo.Console()

    .WriteTo.Seq("http://seq:5341")
    .CreateLogger();

builder.Host.UseSerilog();

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
