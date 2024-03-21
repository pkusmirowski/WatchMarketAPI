using Microsoft.EntityFrameworkCore;
using WatchMarketAPI.Interfaces;
using WatchMarketAPI.Models;
using WatchMarketAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Services
builder.Services.AddScoped<IWatchesContext, WatchesContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy("MyPolicy", builder => {
        builder.WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

//Dependency Injection DbContext Class
builder.Services.AddDbContext<WatchesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
