using Microsoft.EntityFrameworkCore;
using CampusEventHubApi.Data;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => // Dodaje ApplicationDbContext u dependancy injetion controler
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // UseSqlServer konfigurira konekciju sa sql serveron

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



//SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Campus Event Hub API", Version = "v1" });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();  // swagger ui
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Campus Event Hub API v1");
        c.RoutePrefix = "swagger";  // ruta ui na /swagger
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();