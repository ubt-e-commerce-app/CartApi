using Application.Interfaces;
using Microsoft.Extensions.Hosting;

namespace Application.BackgroundJobs;

public class KafkaBackgroundJob : IHostedService
{
    private readonly IKafkaConsumer _kafkaConsumer;

    public KafkaBackgroundJob(IKafkaConsumer kafkaConsumer)
    {
        _kafkaConsumer = kafkaConsumer;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _kafkaConsumer.Consume();
            }
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
