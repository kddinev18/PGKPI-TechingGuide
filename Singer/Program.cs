using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Singer.BusinessLogic.Services;
using Singer.Controller;
using Singer.Infrastructure.Base;
using Singer.Infrastructure.Services;
using Singer.Persistance.Data;
using Singer.Persistance.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISingerService, SingerService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<BaseServiceInjector>();
builder.Services.AddDbContext<ApplicationDbContext>();

IConfiguration configuration = builder.Configuration;

builder.Services
    .AddIdentity<User, IdentityRole>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

IConfigurationSection jwtSection = configuration.GetSection("Jwt");
string issuer = jwtSection["Issuer"]!;
string audience = jwtSection["Audience"]!;
string key = jwtSection["Key"]!;
int expiresInMinutes = int.Parse(jwtSection["ExpiresInMinutes"] ?? "60");

byte[] keyBytes = Encoding.UTF8.GetBytes(key);
SymmetricSecurityKey signingKey = new SymmetricSecurityKey(keyBytes);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = signingKey,
            ClockSkew = TimeSpan.Zero
        };
    });


builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();