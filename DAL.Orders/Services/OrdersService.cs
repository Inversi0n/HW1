using DAL.Orders.DbModels;
using DAL.Orders.Repositories;
using DAL.Orders.Services.Base;
using Newtonsoft.Json;
using Shared.EventModels;

namespace DAL.Orders.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly OrdersRepository _repo;
        private readonly OutboxMessagesRepository _outboxRepository;
        private readonly AppDbContext _context;
        public OrdersService(AppDbContext context)
        {
            _context = context;
            _repo = new OrdersRepository(context);
            _outboxRepository = new OutboxMessagesRepository(context);
        }

    
        public async Task<bool> CreateOrder(Order order)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            await _repo.Insert(order);

            var outbox = new OutboxMessage
            {
                CreatedAt = DateTime.UtcNow,
                MessageType = (int)OutboxType.OrderCreatedEventModel,
                Payload = JsonConvert.SerializeObject(new OrderCreatedOutboxModel
                {
                    OrderId = order.Id
                })
            };

            await _outboxRepository.Insert(outbox);

            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            return true;
        }

        public async Task<bool> Update(params Order[] orders)
        {
            return await _repo.Update(orders);
        }

        public async Task<Order[]> GetByIds(params int[] ids)
        {
            return await _repo.GetByIds(ids);
        }
    }
}
