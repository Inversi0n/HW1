using DAL.Orders.Services.Base;
using Newtonsoft.Json;
using Shared.EventModels;
using Shared.Rabbit;
using Shared.Rabbit.Models;

namespace WorkerService1
{
    internal class OutboxProcessorWorker : BackgroundService
    {
        private readonly IRabbitPublisher _rabbitPublisher;
        private readonly IOutboxMessagesService _outboxService;
        private readonly ILogger<OutboxProcessorWorker> _logger;

        public OutboxProcessorWorker(IOutboxMessagesService outboxService, IRabbitPublisher rabbitPublisher, ILogger<OutboxProcessorWorker> logger)
        {
            _outboxService = outboxService;
            _rabbitPublisher = rabbitPublisher;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await ProcessOutbox(token);
                await Task.Delay(500, token);
            }
        }

        private async Task ProcessOutbox(CancellationToken token)
        {
            var outboxes = await _outboxService.GetUnprocessedOrderOutboxes();

            if (outboxes == null || !(outboxes.Any()) || token.IsCancellationRequested)
                return;

            foreach (var outbox in outboxes)
            {
                var outboxModel = JsonConvert.DeserializeObject<OrderCreatedOutboxModel>(outbox.Payload);

                if (outboxModel == null)
                    continue; //log warning    

                var rabbitPublishModel = new OrderCreatedRabbitModel()
                {
                    OrderId = outboxModel.OrderId
                };
                try
                {
                    _logger.LogInformation("Publishing outbox {OutboxId}", outbox.Id);
                    await _rabbitPublisher.PublishAsync(rabbitPublishModel);
                    _logger.LogInformation("Finish publishing outbox {OutboxId}", outbox.Id);

                    outbox.ProcessedAt = DateTime.UtcNow;

                    await _outboxService.Update(outbox);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to publish outbox {OutboxId}", outbox.Id);
                    // НЕ помечаем ProcessedAt
                }
            }
        }

        /*
         * ТОЛЬКО:
            Читает OutboxMessages            
            Публикует в Rabbit            
            Помечает IsHandled = true    
        
            ❌ НЕ:            
            Бизнес-логика            
            Работа с Orders
         * */
    }

}
