using MediatR;


namespace FactoryMonitoringSystem.Application.Auth.Events.GenerateToken
{
    public record RefreshTokenEvent(string refreshToken) :INotification;
   
}
