using CrudUsers.Context;
using CrudUsers.Middlewares;
using CrudUsers.Repository;
using CrudUsers.Service;
using CrudUsers.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// CORS
var corsSettings = builder.Configuration.GetSection("Cors");
var blazorAppUrl = corsSettings.GetValue<string>("BlazorAppUrl");
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        policy =>
        {
            policy.WithOrigins(blazorAppUrl)
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Controllers y JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

// EF Core
builder.Services.AddDbContextFactory<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.LogTo(Console.WriteLine, LogLevel.Information);
});
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Repositorios
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IActividadesRepository, ActividadesRepository>();

// Servicios
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IActividadesService, ActividadesService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1.0", new OpenApiInfo
    {
        Title = "API Usuarios",
        Version = "v1.0",
        Description = "Si no hay una versión específica, poner 1.0 en el input de versión"
    });

    // API Key config
    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "Ingrese su API Key en el formato: `{key}`",
        Name = "X-API-KEY",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Auto migración
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    dbContext.Database.Migrate();
}

// Middleware pipeline (orden correcto)

app.UseHttpsRedirection();

app.UseCors("AllowBlazorApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "API Usuarios v1.0");
    });
}

// Middleware personalizado para API Key
app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
