using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
{
    var services = builder.Services;
    var configuration = builder.Configuration;

    // SQL Server connection
    services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    // CORS
    services.AddCors();

    // Controllers with JSON options
    services.AddControllers().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

    // AutoMapper
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Swagger/OpenAPI
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "My Web API",
            Version = "v1",
            Description = "API documentation for My Web API"
        });
    });

    // Application services
    services.AddApplicationServices();
}

var app = builder.Build();

// Configure the HTTP request pipeline
{
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // Error handling middleware
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Web API v1");
        c.RoutePrefix = string.Empty; // Swagger UI at root URL
    });
    app.UseStaticFiles();
    app.MapControllers();
}

// Run the app
app.Run("http://localhost:9080");


//dotnet ef migrations add InitialCreate
//dotnet ef database update