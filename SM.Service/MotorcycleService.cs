using SM.Domain.Interface.IRepositories;
using SM.Domain.Interface.IServices;
using SM.Domain.Model;
using SM.Service.Shared;

namespace SM.Service
{
    public class MotorcycleService : ServiceBase<MotorcycleModel>, IMotorcycleService
    {
        private readonly IMotorcycleRepository _repository;

        public MotorcycleService(IMotorcycleRepository Repository) : base(Repository)
        {
            _repository = Repository;
        }

        public Task<MotorcycleModel> GetMotorcycleByPlate(string plate)
        {
           return _repository.GetMotorcycleByPlate(plate);
        }
    }
}
