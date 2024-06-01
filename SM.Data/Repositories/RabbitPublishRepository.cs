using SM.Domain.Interface.IRepositories;
using System.Text;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using SM.Domain.Model.SettingsModel;
using System.Text.Json;

namespace SM.Data.Repositories
{
    public class RabbitPublishRepository : IRabbitPublishRepository
    {
        IConfiguration _configuration;
        public RabbitPublishRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Publish<T>(T message)
        {
            try
            {
                var rabbitMQSettings = _configuration.GetSection("SMRabbitMQConnection").Get<RabbitMQSettings>();

                var factory = new ConnectionFactory
                {
                    HostName = rabbitMQSettings.Hostname,
                    UserName = rabbitMQSettings.User,
                    Password = rabbitMQSettings.Password
                };
                using var connection = factory.CreateConnection();
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "RegisteredMotorcycle",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                    string jsonToString = JsonSerializer.Serialize(message);
                    var body = Encoding.UTF8.GetBytes(jsonToString);

                    channel.BasicPublish(exchange: string.Empty,
                                         routingKey: "RegisteredMotorcycle",
                                         basicProperties: null,
                                         body: body);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
