
namespace FactoryMonitoringSystem.Api
{
    public static class RequestPipeline
    {
        public static IApplicationBuilder UsePresentation(this IApplicationBuilder app)
        {
            app.UseHsts(); // Enforce HSTS in production
            app.UseHttpsRedirection();// HTTPS redirection
            app.UseStaticFiles();// Serve static file
            app.UseRouting();// Routing middleware

            return app;
        }
    }
}
