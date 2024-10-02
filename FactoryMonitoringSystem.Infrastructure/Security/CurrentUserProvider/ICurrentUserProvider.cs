using FactoryMonitoringSystem.Shared;


namespace FactoryMonitoringSystem.Infrastructure.Security.CurrentUserProvider
{
    public interface ICurrentUserProvider
    {
        CurrentUser GetCurrentUser();
    }

}
