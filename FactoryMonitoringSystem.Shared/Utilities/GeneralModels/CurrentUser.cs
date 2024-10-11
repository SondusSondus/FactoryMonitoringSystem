

namespace FactoryMonitoringSystem.Shared
{

    public record CurrentUser(
        Guid Id,
        string Username,
        string Email,
        string Role);
}
