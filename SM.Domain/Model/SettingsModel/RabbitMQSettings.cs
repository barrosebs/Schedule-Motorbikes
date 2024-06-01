
namespace SM.Domain.Model.SettingsModel
{
    public class RabbitMQSettings
    {
        public required string Hostname { get; set; }
        public int Port { get; set; }
        public required string User { get; set; }
        public required string Password { get; set; }
    }
}
