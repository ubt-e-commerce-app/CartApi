using Confluent.Kafka;
using System.Threading;

namespace Application.Kafka;

public class KafkaConsumer
{
    public void Consume()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "foo",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
        {
            consumer.Subscribe("AddToCart");

            while (true)
            {
                var consumeResult = consumer.Consume();



            }
        }

    }
}
