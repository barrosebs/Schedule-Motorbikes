using Microsoft.EntityFrameworkCore;
using SM.Data.Context;
using SM.Domain.Interface.IRepositories;
using SM.Domain.Model;

namespace SM.Data.Repositories
{
    public class AllocationRepository : RepositoryBase<AllocationModel>, IAllocationRepository
    {

        public AllocationRepository(SMContext context) : base(context) { }

        public async Task<AllocationModel> GetAllocationAsync()
        {
            var deliveryPersonWithAllocation = _context.Allocations
                                                .Include(dp => dp.DeliveryPerson)
                                                .Include(dp => dp.Motorcycle)
                                                .FirstOrDefault(dp => dp.IsAllocation == true);

            return deliveryPersonWithAllocation;

        } 
    }
}
