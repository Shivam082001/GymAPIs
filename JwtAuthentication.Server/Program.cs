using JwtAuthentication.Server.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<GymManagementSystemContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GymManagementSystemConStr")));

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:5001",
            ValidAudience = "https://localhost:5001",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
        };
    });


builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder => 
    { 
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod(); 
    });
});

builder.Services.AddControllers().AddJsonOptions(
    options =>
    {
        { options.JsonSerializerOptions.PropertyNamingPolicy = null; };
        { options.JsonSerializerOptions.DictionaryKeyPolicy = null; };
    }
    );

//builder.Services.AddControllers();

//var app = builder.Build();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwaggerUI;
//    app.UseSwaggerUI();
//}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

//app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
app.UseCors("EnableCORS");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
