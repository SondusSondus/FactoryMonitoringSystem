
namespace FactoryMonitoringSystem.Shared.Utilities.GeneralModels
{
    public record ConcurrencyConflict
    {
       
        public EmailModel EmailModel { get; set; }
        public InAppNotificationModel InAppNotificationModel { get; set; }

        public ConcurrencyConflict(string entityName, string userEmail)
        {
        
            EmailModel = new(userEmail, $"Concurrency Conflict on {entityName}", $"A concurrency conflict occurred on {entityName}.");
            InAppNotificationModel = new InAppNotificationModel(userEmail, $"Concurrency conflict detected on {entityName}. Please review the changes.");
        }
    }
}
