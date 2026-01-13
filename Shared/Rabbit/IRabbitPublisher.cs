using Shared.Rabbit.Models;

namespace Shared.Rabbit
{
    public interface IRabbitPublisher
    {
        Task PublishAsync(OrderCreatedRabbitModel orderCreatedEventModel);
    }
}
