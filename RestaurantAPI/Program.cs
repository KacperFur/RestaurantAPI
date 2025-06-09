using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Data;
using RestaurantAPI.Infrastructure.Extensions;
using RestaurantAPI.Application.Mappings;
using RestaurantAPI.Entities;
using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Infrastructure.Presistence;
using FluentValidation.AspNetCore;
using FluentValidation;
using RestaurantAPI.Application.Validators;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Serilog;
using RestaurantAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();
builder.Services.AddApplicationServices();
builder.Services.AddApplicationRepositories();
builder.Services
    .AddHealthChecks().AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(10); // jak czêsto odpytywaæ
    options.MaximumHistoryEntriesPerEndpoint(60);
    options.AddHealthCheckEndpoint("API", "/health");
}).AddInMemoryStorage();




builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireAssertion(context =>
        {
            var roleIdClaim = context.User.FindFirst("role_id")?.Value;
            return roleIdClaim == "3"; 
        }));
    options.AddPolicy("AdminOrManager", policy =>
        policy.RequireAssertion(context =>
        {
            var roleIdClaim = context.User.FindFirst("role_id")?.Value;
            return roleIdClaim == "2" || roleIdClaim == "3"; 
        }));
    options.AddPolicy("AnyStaff", policy =>
        policy.RequireAssertion(context =>
        {
            var roleIdClaim = context.User.FindFirst("role_id")?.Value;
            return roleIdClaim == "1" || roleIdClaim == "2" || roleIdClaim == "3"; 
        }));
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c=>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Warning()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantAPI V1");
    });
    app.MapOpenApi();
}
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";
});

app.Run();
