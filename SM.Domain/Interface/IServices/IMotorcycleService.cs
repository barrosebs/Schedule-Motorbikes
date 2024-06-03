using SM.Domain.Interface.IRepository;
using SM.Domain.Model;

namespace SM.Domain.Interface.IServices
{
    public interface IMotorcycleService : IRepositoryBase<MotorcycleModel> {
        Task<MotorcycleModel> GetMotorcycleByPlate(string plate);
    }
}
