using Microsoft.EntityFrameworkCore;
using SM.Data.Context;
using SM.Domain.Interface.IRepositories;
using SM.Domain.Model;
namespace SM.Data.Repositories
{
    public class DeliveryPersonRepository : RepositoryBase<DeliveryPersonModel>, IDeliveryPersonRepository
    {
        public DeliveryPersonRepository(SMContext context) : base(context) { }

        public async Task<DeliveryPersonModel> GetDeliveryPersonByEmail(string email)
        {
            return await _context.DeliveryPeople.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
