using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SM.Console.RabbitMQConsumer.Interface;
using SM.Domain.Model;

namespace SM.Console.RabbitMQConsumer
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly IConfiguration _configuration;

        public MotorcycleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task CreateAsync(MotorcycleModel motorcycle)
        {
            try
            {
                motorcycle.Id = Guid.NewGuid();
                var connectionString = _configuration.GetConnectionString("SMConnection");
                await using var conn = new NpgsqlConnection(connectionString);
                await conn.OpenAsync();
                await using var cmd = new NpgsqlCommand("INSERT INTO motorcycle (id,model, year,license_plate,IsDelivered, datecreated) VALUES (@Id,@model, @year, @licenseplate,@IsDelivered,@datecreated)", conn);
                // Definição dos parâmetros
                cmd.Parameters.AddWithValue("@Id", motorcycle.Id);
                cmd.Parameters.AddWithValue("@model", motorcycle.Model);
                cmd.Parameters.AddWithValue("@year", motorcycle.Year);
                cmd.Parameters.AddWithValue("@licenseplate", motorcycle.LicensePlate);
                cmd.Parameters.AddWithValue("@IsDelivered", motorcycle.IsDelivered);
                cmd.Parameters.AddWithValue("@datecreated", motorcycle.DateCreated);

                await cmd.ExecuteNonQueryAsync();
            }
            catch (PostgresException exPostgres)
            {
                
                throw exPostgres;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
