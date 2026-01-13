using DAL.Orders.DbModels;
using DAL.Orders.Repositories;
using DAL.Orders.Services.Base;
using Shared.EventModels;

namespace DAL.Orders.Services
{
    public class OutboxMessagesService : IOutboxMessagesService
    {
        private readonly OutboxMessagesRepository _repo;

        public OutboxMessagesService(AppDbContext context)
        {
            _repo = new OutboxMessagesRepository(context);
        }

        public async Task<bool> Insert(params OutboxMessage[] ordoutboxesrs)
        {
            return await _repo.Insert(ordoutboxesrs);
        }
        public async Task<bool> Update(params OutboxMessage[] outboxes)
        {
            return await _repo.Update(outboxes);
        }

        public async Task<OutboxMessage[]> GetByIds(params int[] ids)
        {
            return await _repo.GetByIds(ids);
        }

        public async Task<OutboxMessage[]> GetUnprocessedOrderOutboxes()
        {
            var maxCount = 10;
            return await _repo.GetUnprocessed((int)OutboxType.OrderCreatedEventModel, maxCount);
        }
    }
}
