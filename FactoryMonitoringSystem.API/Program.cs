using FactoryMonitoringSystem.Api;
using FactoryMonitoringSystem.Api.Middlewares;
using FactoryMonitoringSystem.Application;
using FactoryMonitoringSystem.Infrastructure;
using FactoryMonitoringSystem.Infrastructure.Cache;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation(builder.Configuration, builder.Host)
                    .AddApplication()
                    .AddInfrastructure(builder.Configuration, builder.Host);
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionMiddleware>();// Error handling middleware
    app.UseMiddleware<CachingMiddleware>(); 

    app.UsePresentation();
    app.UseInfrastructure();

    app.MapControllers();

    app.Run();
}
