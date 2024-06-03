
using Microsoft.EntityFrameworkCore;
using SM.Data.Context;
using SM.Domain.Interface.IRepositories;
using SM.Domain.Model;

namespace SM.Data.Repositories
{
    public class MotorcycleRepository : RepositoryBase<MotorcycleModel>, IMotorcycleRepository
    {
        public MotorcycleRepository(SMContext context) : base(context) { }

        public async Task<MotorcycleModel> GetMotorcycleByPlate(string plate)
        {
            MotorcycleModel motorcycle = await _context.Motorcycles.FirstOrDefaultAsync(p => p.LicensePlate.ToUpper().Equals(plate.ToUpper()));
            return motorcycle;
        }
    }
}
