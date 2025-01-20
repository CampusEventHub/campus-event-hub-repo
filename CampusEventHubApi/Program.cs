using CampusEventHubApi.Data;
using CampusEventHubApi.Services;
using CampusEventHubApi.Services.Decorators;
using CampusEventHubApi.Services.Interfaces;
using CampusEventHubApi.Services.Observers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Scrutor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Singleton Pattern: Registracija ApplicationDbContext kao Singleton
builder.Services.AddSingleton<ApplicationDbContext>(provider =>
{
    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    return new ApplicationDbContext(optionsBuilder.Options);
});

// Komentar: Singleton osigurava da postoji samo jedna instanca ApplicationDbContext kroz cijelu aplikaciju.

// Dodavanje servisa u Dependency Injection
builder.Services.AddScoped<IUserManagementService, UserManagementService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IEventManagementService, EventService>();
builder.Services.AddScoped<IOcjenaService, OcjenaService>();

// Registracija KalendarService i dekoratora
builder.Services.AddScoped<IKalendarService, KalendarService>();
builder.Services.Decorate<IKalendarService, ValidationDecorator>();

// Registracija OcjenaService i OcjenaValidationDecorator
builder.Services.AddScoped<IOcjenaService, OcjenaService>();
builder.Services.Decorate<IOcjenaService, OcjenaValidationDecorator>();


// Dodavanje EmailNotifier kao Singleton observera
var emailNotifier = new EmailNotifier();
builder.Services.AddSingleton<IEventObserver>(emailNotifier);

// Registracija NotificationManager kao Singleton
builder.Services.AddSingleton(NotificationManager.Instance);

// Dodavanje kontrolera
builder.Services.AddControllers();

// CORS konfiguracija
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// JWT autentifikacija
string secureKey = builder.Configuration["JWT:SecureKey"];
if (string.IsNullOrEmpty(secureKey))
{
    throw new InvalidOperationException("JWT SecureKey nije konfigurisan u appsettings.json.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey))
        };
    });

// Swagger konfiguracija sa JWT podrškom
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Campus Event Hub API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Unesite JWT token u formatu: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Campus Event Hub API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

// Registracija observera u KalendarService
var serviceProvider = builder.Services.BuildServiceProvider();
var kalendarService = serviceProvider.GetService<IKalendarService>();
kalendarService?.RegisterObserver(emailNotifier);



app.MapControllers();

app.Run();
