using PortfolioProApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PortfolioProApi.JwtServices;
using PortfolioProApi.data;
using Microsoft.EntityFrameworkCore;
using PortfolioProApi.Entities;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
      .AllowAnyHeader()
      .AllowAnyMethod();
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var secrets = builder.Configuration.GetSection("WebApiOptions");

builder.Services.Configure<WebApiOptions>(secrets);



builder.Services.AddIdentity<User, IdentityRole>(cfg =>
{
    cfg.User.RequireUniqueEmail = true;
    //cfg.User.{add More Constraints here}
})
    .AddEntityFrameworkStores<PortfolioProApiContext>();

var jwt = builder.Configuration.GetSection("JwtSettings");  //found in appsettings.json


builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwt.GetSection("validIssuer").Value,
        ValidAudience = jwt.GetSection("validAudience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.GetSection("securityKey").Value))
    };
});

builder.Services.AddScoped<JwtHandler>();
builder.Services.AddDbContext<PortfolioProApiContext>();


var app = builder.Build();

app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
