using DAL.Orders.Extentions;
using DAL.Orders.Services.Base;
using Newtonsoft.Json;
using Shared.Rabbit;
using Shared.Rabbit.Models;

namespace WorkerService1
{
    public class OrdersConsumerWorker : BackgroundService
    {
        private readonly IRabbitConsumer _consumer;
        private readonly IOrdersService _ordersService;
        private readonly ILogger<OrdersConsumerWorker> _logger;

        public OrdersConsumerWorker(IRabbitConsumer consumer, IOrdersService ordersService, ILogger<OrdersConsumerWorker> logger)
        {
            _ordersService = ordersService;
            _consumer = consumer;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.SubscribeAsync("orders.created", async msg =>
            {
                if (stoppingToken.IsCancellationRequested)
                    return;

                var orderRabbit = JsonConvert.DeserializeObject<OrderCreatedRabbitModel>(msg);

                var order = await _ordersService.GetByIds(orderRabbit.OrderId).GetSingleAsync();

                if (order.ConfirmedAt.HasValue || stoppingToken.IsCancellationRequested)
                    return;


                order.ConfirmedAt = DateTime.UtcNow;

                var updated = await _ordersService.Update(order);
                // обработка
            }, stoppingToken);

            /*
             * ТОЛЬКО:
                Слушает Rabbit
                Делает бизнес-логику (уведомления, интеграции, read models и т.д.)
             * */
        }
    }
}