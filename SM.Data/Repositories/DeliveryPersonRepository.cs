using SM.Data.Context;
using SM.Domain.Interface.IRepositories;
using SM.Domain.Model;

namespace SM.Data.Repositories
{
    public class DeliveryPersonRepository : RepositoryBase<DeliveryPersonModel>, IDeliveryPersonRepository
    {
        public DeliveryPersonRepository(SMContext context) : base(context) { }
    }
}
