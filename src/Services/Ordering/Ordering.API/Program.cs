var builder = WebApplication.CreateBuilder(args);

// Add services to the container

var app = builder.Build();

// Configure HTTP Request pipeline

app.Run();
