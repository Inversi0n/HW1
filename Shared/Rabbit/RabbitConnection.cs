using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Shared.Rabbit
{
    public class RabbitConnection : IRabbitConnection
    {
        public IConnection Connection { get; }

        public RabbitConnection(IConfiguration config)
        {
            var factory = new ConnectionFactory
            {
                HostName = config["Rabbit:Host"]
            };

            Connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            this.Connection.Dispose();
        }
    }
}
