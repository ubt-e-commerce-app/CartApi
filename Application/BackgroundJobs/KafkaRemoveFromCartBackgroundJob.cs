using Application.Interfaces;
using Application.Kafka;
using Microsoft.Extensions.Hosting;

namespace Application.BackgroundJobs;

public class KafkaRemoveFromCartBackgroundJob : IHostedService
{
    private readonly IKafkaRemoveFromCartConsumer _kafkaRemoveFromCartConsumer;

    public KafkaRemoveFromCartBackgroundJob(IKafkaRemoveFromCartConsumer kafkaRemoveFromCartConsumer)
    {
        _kafkaRemoveFromCartConsumer = kafkaRemoveFromCartConsumer;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
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
