using FoodService.Data;
using FoodService.Extensions;
using FoodService.Repositories;
using Microsoft.Extensions.Options;
//using Plain.RabbitMQ;
using RabbitMQ.Client;
using MessageBrokerLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<DatabaseSettings>(
        builder.Configuration.GetSection(nameof(DatabaseSettings)));

builder.Services.AddSingleton<IDatabaseSettings>(provider =>
        provider.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddScoped<IFoodCategoryRepository, FoodCategoryRepository>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();

builder.Services.AddSingleton<IConnectionProvider>(new ConnectionProvider("amqp://guest:guest@localhost:5672"));
builder.Services.AddSingleton<IPublisher>(x => new Publisher(x.GetService<IConnectionProvider>(),
    "order-exchange",
    ExchangeType.Topic));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>(); // custom global exception

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
