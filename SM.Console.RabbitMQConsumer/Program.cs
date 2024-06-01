using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SM.Console.RabbitMQConsumer;
using SM.Console.RabbitMQConsumer.Interface;
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        // Obter a configuração e o repositório dos serviços
        var configuration = host.Services.GetRequiredService<IConfiguration>();
        var motorcycleRepository = host.Services.GetRequiredService<IMotorcycleRepository>();

        // Inicializar o RabbitConsumer
        RabbitConsumer.Initialize(configuration, motorcycleRepository);

        // Iniciar o RabbitConsumer
        RabbitConsumer.Start();

        // Manter a aplicação em execução
        Console.WriteLine("Press [enter] to exit.");
        Console.ReadLine();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                builder.AddEnvironmentVariables();
                if (args != null)
                {
                    builder.AddCommandLine(args);
                }
            })
            .ConfigureServices((hostContext, services) =>
            {
                // Registrar os serviços necessários
                services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();
            });
}
