using Application.Interfaces;
using Confluent.Kafka;

namespace Application.Kafka;

public class KafkaConsumer : IKafkaConsumer
{
    public void Consume()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "foo"
        };

        using (var consumer = new ConsumerBuilder<string, string>(config).Build())
        {
            consumer.Subscribe("AddToCart");

            while (true)
            {
                try
                {
                    var message = consumer.Consume();
                    Console.WriteLine($"Received message: {message.Value}");
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occured: {e.Error.Reason}");
                }
            }
        }

    }
}
