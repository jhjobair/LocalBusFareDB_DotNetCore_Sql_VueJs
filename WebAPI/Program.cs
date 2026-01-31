using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using WebApi.Helpers;
using WebApi.Interface;
using WebApi.Services;
using WebApi.Models.jwtToken;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
{
    Environment.SetEnvironmentVariable(
        "REPORTING_VBC_PATH",
        @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\vbc.exe",
        EnvironmentVariableTarget.Process
    );

    var services = builder.Services;
    var configuration = builder.Configuration;
    System.Text.Encoding.RegisterProvider(
    System.Text.CodePagesEncodingProvider.Instance);

    // SQL Server connection
    services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
            ValidateIssuerSigningKey = true
        };
    });
    // This line maps the "SmtpSettings" section from appsettings.json to the SmtpSettings class


    builder.Services.AddScoped<IEmailService, EmailService>();
    builder.Services.AddScoped<IAuthService, AuthService>();


    builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

    // CORS
    services.AddCors(options =>
    {
        options.AddPolicy("*",
            policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });

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