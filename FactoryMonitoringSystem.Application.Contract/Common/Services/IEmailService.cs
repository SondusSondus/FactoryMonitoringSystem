using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;


namespace FactoryMonitoringSystem.Application.Contracts.Common.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailModel emailModel,CancellationToken cancellationToken);

    }
}
