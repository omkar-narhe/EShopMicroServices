﻿namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Register API services here
        // services.AddControllers();
        // services.AddSwaggerGen();
        // services.AddCarter();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication webApplication)
    {
        // app.MapCarter();

        return webApplication;
    }
} 
