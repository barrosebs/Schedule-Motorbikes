using SM.Data.Context;
using SM.Domain.Interface.IRepositories;
using SM.Domain.Model;

namespace SM.Data.Repositories
{
    public class AllocationRepository : RepositoryBase<AllocationModel>, IAllocationRepository
    {
        public AllocationRepository(SMContext context) : base(context) { }
    }
}
