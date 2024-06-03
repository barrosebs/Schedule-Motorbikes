using SM.Domain.Interface.IRepository;
using SM.Domain.Model;

namespace SM.Domain.Interface.IRepositories
{
    public interface IMotorcycleRepository : IRepositoryBase<MotorcycleModel> {
        Task<MotorcycleModel> GetMotorcycleByPlate(string plate);

    }
}
