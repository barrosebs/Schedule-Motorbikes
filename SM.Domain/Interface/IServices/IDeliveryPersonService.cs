using SM.Domain.Interface.IRepository;
using SM.Domain.Model;

namespace SM.Domain.Interface.IService
{
    public interface IDeliveryPersonService : IRepositoryBase<DeliveryPersonModel> {
        Task<DeliveryPersonModel> GetDeliveryPersonByEmail(string email);
    }
}
