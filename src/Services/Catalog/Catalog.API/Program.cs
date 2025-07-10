var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP Pipeline
app.MapCarter();
app.Run();
