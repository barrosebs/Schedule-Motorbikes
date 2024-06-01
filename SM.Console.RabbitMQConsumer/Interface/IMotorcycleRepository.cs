using SM.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Console.RabbitMQConsumer.Interface
{
    public interface IMotorcycleRepository
    {
        Task CreateAsync(MotorcycleModel motorcycle);
    }
}
