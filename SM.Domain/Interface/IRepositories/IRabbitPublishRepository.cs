namespace SM.Domain.Interface.IRepositories
{
    public interface IRabbitPublishRepository
    {
        void Publish<T>(T message);
    }
}
