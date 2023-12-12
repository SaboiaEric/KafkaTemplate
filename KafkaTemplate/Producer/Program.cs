using Confluent.Kafka;
using Domain;
using System.Text.Json;

namespace Producer
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Iniciando o produtor!");

            var configuration = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            var producer = new ProducerBuilder<string, string>(configuration).Build();

            try
            {
                var clima = new Clima("Verao", 70);

                var result = producer.ProduceAsync("clima-tempo",
                    new Message<string, string> { Value = JsonSerializer.Serialize(clima) },
                    CancellationToken.None);

                Console.WriteLine($"produziu o evento.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unhandled exception occurs: {ex.Message}", ex);
            }
        }
    }
}