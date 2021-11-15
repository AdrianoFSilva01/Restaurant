using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Restaurant.API.Persistence;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RestaurantDbContext>(
options => options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
