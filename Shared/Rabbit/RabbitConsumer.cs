using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Shared.Rabbit
{
    public class RabbitConsumer : IRabbitConsumer
    {
        private readonly IRabbitConnection _factory;

        public RabbitConsumer(IRabbitConnection factory)
        {
            _factory = factory;
        }

        public async Task SubscribeAsync(
            string queue,
            Func<string, Task> handler,
            CancellationToken token)
        {
            using var channel = await _factory.Connection.CreateChannelAsync();

            //await channel.QueueDeclareAsync(queue, durable: true, exclusive: false);
            await channel.QueueDeclareAsync(
                queue: queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                await handler(body);
                await channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            var isExclusive = false;
            var isNoLocal = false;
            //var resStr = await channel.BasicConsumeAsync(queue, false, consumer.ConsumerTags.FirstOrDefault(), isNoLocal, isExclusive, null, consumer, token);

            await channel.BasicConsumeAsync(
                queue: queue,
                autoAck: false,
                consumer: consumer);
        }
    }
}
