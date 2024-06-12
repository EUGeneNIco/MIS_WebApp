using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MIS.Application._Interfaces;
using MIS.Application._Mappings.AutoMapper;
using MIS.Domain;
using MIS.Persistence;
using MIS.Persistence.Implementations;
using MIS.WebAPI;
using MIS.WebAPI.Auth;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.GetSection("Settings").Get<AppSettingsClass>();
var JWT_KEY = builder.Configuration.GetSection("Key").Get<string>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Management Information System (MIS)", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert bearer token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[]{ }
        }
    });
});

builder.Services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); // RMF: Fix for "Possible object cycle was detected which is not supported"


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(settings.DefaultConnectionStrings)
                    .UseLazyLoadingProxies());

builder.Services.AddScoped(typeof(IAppDbContext), typeof(AppDbContext));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWT_KEY)),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        x.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["mis_auth"];
                return Task.CompletedTask;
            }
        };
    });

// JwtAuthenticationManager
builder.Services.AddScoped<IJwtAuthenticationManager>(manager => new JwtAuthenticationManager(JWT_KEY, manager.GetService<IMediator>()));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Register all MediatR dependencies
var assembly = AppDomain.CurrentDomain.Load("MIS.Application"); // Use this approach because the handlers are in a separate assembly
builder.Services.AddMediatR(assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(builder => builder.WithOrigins(settings.CLIENT_URL).AllowAnyHeader().AllowAnyMethod().AllowCredentials());

app.Run();
