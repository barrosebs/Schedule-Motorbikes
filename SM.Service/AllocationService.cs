using SM.Domain.Interface.IRepositories;
using SM.Domain.Interface.IServices;
using SM.Domain.Model;
using SM.Service.Shared;
namespace SM.Service
{
    internal class AllocationService : ServiceBase<AllocationModel>, IAllocationService
    {
        private readonly IAllocationRepository _repository;

        public AllocationService(IAllocationRepository Repository) : base(Repository)
        {
            _repository = Repository;
        }
    }
}
