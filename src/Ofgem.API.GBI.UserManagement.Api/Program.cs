using Ofgem.API.GBI.UserManagement.Api.Extensions;
using Ofgem.API.GBI.UserManagement.Api.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
builder.Services.ConfigureApplicationServices(builder.Configuration);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen();
}

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapExternalUserEndpoints();
app.MapSupplierEndpoints();

app.Run();

public partial class Program { }
