var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure the HTTP Pipeline
app.MapCarter();
app.Run();
