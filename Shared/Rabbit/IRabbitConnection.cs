using RabbitMQ.Client;

namespace Shared.Rabbit
{
    public interface IRabbitConnection : IDisposable
    {
        IConnection Connection { get; }
    }
}
