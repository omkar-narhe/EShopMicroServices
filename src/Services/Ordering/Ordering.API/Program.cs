

using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container


// -----------------------------------------

// Infrastructure - EF Core
// Application - MediatR
// API - Carter, HealtChecks, ...
// -----------------------------------------


builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();


var app = builder.Build();

// Configure HTTP Request pipeline

app.Run();
