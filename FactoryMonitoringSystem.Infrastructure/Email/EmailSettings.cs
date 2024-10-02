

namespace FactoryMonitoringSystem.Infrastructure.Email
{
    public record EmailSettings
    {
        public const string Section = "EmailSettings";

        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromEmail { get; set; }
        public bool EnableSsl { get; set; }
    }

}
