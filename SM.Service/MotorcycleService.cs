using SM.Domain.Interface.IRepositories;
using SM.Domain.Interface.IService;
using SM.Domain.Interface.IServices;
using SM.Domain.Model;
using SM.Service.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Service
{
    public class MotorcycleService : ServiceBase<MotorcycleModel>, IMotorcycleService
    {
        private readonly IMotorcycleRepository _repository;

        public MotorcycleService(IMotorcycleRepository Repository) : base(Repository)
        {
            _repository = Repository;
        }
    }
}
