using SM.Domain.Interface.IRepository;
using SM.Domain.Model;

namespace SM.Domain.Interface.IRepositories
{
    public interface IDeliveryPersonRepository : IRepositoryBase<DeliveryPersonModel> {
        Task<DeliveryPersonModel> GetDeliveryPersonByEmail(string email);
    }
}
