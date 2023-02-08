using Application.Exceptions;
using Application.Interfaces;
using Application.Requests;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace Application.Kafka;

public class KafkaAddToCartConsumer : IKafkaAddToCartConsumer
{
    private readonly ICartService _cartService;

    public KafkaAddToCartConsumer(ICartService cartService)
    {
        _cartService = cartService;
    }

    public void Consume()
    {
        try
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

                        if (!string.IsNullOrEmpty(message.Message.Value))
                        {
                            var deserializedMessage = JsonConvert.DeserializeObject<AddToCartRequest>(message.Message.Value);

                            if (deserializedMessage != null)
                                _cartService.AddToCart(deserializedMessage);
                        }
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occured: {e.Error.Reason}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new KafkaExceptions();
        }
    }
}
