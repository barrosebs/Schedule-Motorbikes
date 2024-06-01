﻿using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SM.Console.RabbitMQConsumer.Interface;
using SM.Domain.Model;
using SM.Domain.Model.SettingsModel;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public static class RabbitConsumer
{
    private static IConfiguration _configuration;
    private static IMotorcycleRepository _motorcycleRepository;

    public static void Initialize(IConfiguration configuration, IMotorcycleRepository motorcycleRepository)
    {
        _configuration = configuration;
        _motorcycleRepository = motorcycleRepository;
    }

    public static void Start()
    {
        if (_configuration == null || _motorcycleRepository == null)
        {
            throw new InvalidOperationException("RabbitConsumer is not initialized.");
        }

        var rabbitMQSettings = _configuration.GetSection("SMRabbitMQConnection").Get<RabbitMQSettings>();
        var factory = new ConnectionFactory
        {
            HostName = rabbitMQSettings.Hostname,
            UserName = rabbitMQSettings.User,
            Password = rabbitMQSettings.Password
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "RegisteredMotorcycle",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var motorcycle = JsonSerializer.Deserialize<MotorcycleModel>(message);
                if (motorcycle.Year == "2024")
                {
                    await _motorcycleRepository.CreateAsync(motorcycle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing message: {ex.Message}");
            }
        };

        channel.BasicConsume(queue: "RegisteredMotorcycle",
                             autoAck: true,
                             consumer: consumer);

        Console.WriteLine("Consumer started. Press [enter] to exit.");
        Console.ReadLine();
    }
}
