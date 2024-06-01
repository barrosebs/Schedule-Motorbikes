using SM.Domain.Interface.IRepositories;
using SM.Domain.Interface.IServices;
namespace SM.Service.RabbitMQ
{
    public class RabbitPublishService : IRabbitPublishService
    {
        private readonly IRabbitPublishRepository _repository;

        public RabbitPublishService(IRabbitPublishRepository repository)
        {
            _repository = repository;
        }

        public void Publish<T>(T message)
        {
            _repository.Publish(message);
        }
    }
}
