using SM.Domain.Interface.IRepositories;
using SM.Domain.Interface.IService;
using SM.Domain.Model;
using SM.Service.Shared;

namespace SM.Service
{
    public class DeliveryPersonService : ServiceBase<DeliveryPersonModel>, IDeliveryPersonService {
        private readonly IDeliveryPersonRepository _repository;

        public DeliveryPersonService(IDeliveryPersonRepository Repository) : base(Repository)
        {
            _repository = Repository;
        }
    }
}
