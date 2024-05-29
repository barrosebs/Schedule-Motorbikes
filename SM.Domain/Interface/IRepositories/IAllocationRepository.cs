using SM.Domain.Interface.IRepository;
using SM.Domain.Model;

namespace SM.Domain.Interface.IRepositories
{
    public interface IAllocationRepository : IRepositoryBase<AllocationModel> { 
    Task<AllocationModel> GetAllocationAsync();
    }
}
