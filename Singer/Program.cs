using Microsoft.AspNetCore.Mvc;
using Singer.BusinessLogic.Services;
using Singer.Controller;
using Singer.Infrastructure.Base;
using Singer.Infrastructure.Services;
using Singer.Persistance.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISingerService, SingerService>();
builder.Services.AddScoped<BaseServiceInjector>();
builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();