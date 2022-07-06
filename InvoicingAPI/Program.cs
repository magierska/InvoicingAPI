using InvoicingAPI.CosmosDb;
using InvoicingAPI.CosmosDb.Models;
using InvoicingAPI.CosmosDb.Repositories;
using InvoicingAPI.Domain.Abstractions;
using InvoicingAPI.Filters;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add console logging.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add configuration
builder.Services.Configure<DbConfiguration>(builder.Configuration.GetSection("CosmosDb"));

// Add services to the container.
builder.Services.AddControllers(options => options.Filters.Add<ExceptionFilter>()).AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenNewtonsoftSupport();

var assembly = Assembly.GetExecutingAssembly();
builder.Services.AddMediatR(assembly);
builder.Services.AddAutoMapper(assembly, Assembly.GetAssembly(typeof(InvoicingAPI.CosmosDb.Profiles.CustomersProfile)));

builder.Services.AddTransient<IInvoicingDbRepository, InvoicingDbRepository>();
builder.Services.AddTransient<DbInitializer>();
builder.Services.AddSingleton(serviceProvider =>
{
    var dbConfiguration = serviceProvider.GetService<IOptions<DbConfiguration>>()?.Value ?? throw new ConfigurationErrorsException("Missing configuration: 'CosmosDb'.");
    return new CosmosClient(dbConfiguration.EndpointUri, dbConfiguration.PrimaryKey, new CosmosClientOptions
    {
        SerializerOptions = new CosmosSerializationOptions()
        {
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
        }
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetService<DbInitializer>() ?? throw new InvalidOperationException($"{typeof(DbInitializer)} is not registered.");
    await initializer.InitAsync();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();