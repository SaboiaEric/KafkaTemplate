using Confluent.Kafka;
using Domain;
using System.Text.Json;

namespace Consumer
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Iniciando o consumidor");

            var configuration = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = "group-id"
            };

            var consumer = new ConsumerBuilder<string, string>(configuration).Build();

            consumer.Subscribe("clima-tempo");

            try
            {
                while (true)
                {
                    var clima = new Clima("Verao", 70);

                    var result = consumer.Consume(CancellationToken.None);
                    var information = JsonSerializer.Deserialize<Clima>(result.Value);
                    Console.WriteLine($"consumiu o evento {information.name} com valor: {information.valor}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unhandled exception occurs: {ex.Message}", ex);
            }
        }
    }
}