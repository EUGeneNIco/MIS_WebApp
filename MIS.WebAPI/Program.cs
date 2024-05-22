using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Helpers;
using MIS.Domain;
using MIS.Persistence;
using MIS.WebAPI;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.GetSection("Settings").Get<AppSettingsClass>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(settings.DefaultConnectionStrings));

builder.Services.AddScoped(typeof(IAppDbContext), typeof(AppDbContext));

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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

app.UseAuthorization();

app.MapControllers();

app.UseCors(builder => builder.WithOrigins(settings.CLIENT_URL).AllowAnyHeader().AllowAnyMethod().AllowCredentials());

app.Run();
