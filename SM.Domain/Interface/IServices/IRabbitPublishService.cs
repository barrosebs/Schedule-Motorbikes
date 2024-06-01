namespace SM.Domain.Interface.IServices
{
    public interface IRabbitPublishService
    {
        void Publish<T>(T message);
    }
}
