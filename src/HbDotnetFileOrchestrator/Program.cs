using HbDotnetFileOrchestrator.Application.Extensions;
using HbDotnetFileOrchestrator.Infrastructure.Extensions;
using HbDotnetFileOrchestrator.Modules.V1;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder =>
    loggingBuilder.AddSerilog(dispose: true));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();
builder.Services.AddHttpContextAccessor();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

var apiGrouping = app.MapGroup("api");
apiGrouping.MapHealthChecks("/health");

var v1Grouping = apiGrouping.MapGroup("v1");
v1Grouping.MapV1FilesModule();

app.Run();