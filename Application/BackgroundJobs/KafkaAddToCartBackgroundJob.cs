using Application.Interfaces;
using Microsoft.Extensions.Hosting;

namespace Application.BackgroundJobs;

public class KafkaAddToCartBackgroundJob : IHostedService
{
    private readonly IKafkaAddToCartConsumer _kafkaAddToCartConsumer;
    private readonly IKafkaRemoveFromCartConsumer _kafkaRemoveFromCartConsumer;

    public KafkaAddToCartBackgroundJob(IKafkaAddToCartConsumer kafkaConsumer, IKafkaRemoveFromCartConsumer kafkaRemoveFromCartConsumer)
    {
        _kafkaAddToCartConsumer = kafkaConsumer;
        _kafkaRemoveFromCartConsumer = kafkaRemoveFromCartConsumer;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _kafkaAddToCartConsumer.Consume();
                _kafkaRemoveFromCartConsumer.Consume();
            }
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
