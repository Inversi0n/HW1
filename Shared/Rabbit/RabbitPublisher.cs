using RabbitMQ.Client;
using Shared.Rabbit.Models;
using System.Text;
using System.Text.Json;

namespace Shared.Rabbit
{
    public class RabbitPublisher : IRabbitPublisher
    {
        private readonly IRabbitConnection _factory;

        public RabbitPublisher(IRabbitConnection factory)
        {
            _factory = factory;
        }

        public async Task PublishAsync(OrderCreatedRabbitModel orderCreatedEventModel)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            using var channel = await _factory.Connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: "orders.exchange",
                type: ExchangeType.Direct,
                durable: true);

            var message = JsonSerializer.Serialize(orderCreatedEventModel);
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(
               exchange: "orders.exchange",
               routingKey: "order.created",
               body: body);
        }
    }
}
