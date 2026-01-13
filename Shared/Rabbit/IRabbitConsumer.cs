
namespace Shared.Rabbit
{
    public interface IRabbitConsumer
    {
        Task SubscribeAsync(string queue, Func<string, Task> handler, CancellationToken token);
    }
}