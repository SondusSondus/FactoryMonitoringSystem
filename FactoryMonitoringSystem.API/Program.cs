using FactoryMonitoringSystem.Api;
using FactoryMonitoringSystem.Application;
using FactoryMonitoringSystem.Infrastructure;
using FactoryMonitoringSystem.Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation(builder.Configuration,builder.Host)
                    .AddApplication()
                    .AddInfrastructure(builder.Configuration);
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
    app.UsePresentation();
    app.UseInfrastructure();

    app.MapControllers();

    app.Run();
}
