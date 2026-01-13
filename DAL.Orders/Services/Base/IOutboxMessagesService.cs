using DAL.Orders.DbModels;

namespace DAL.Orders.Services.Base
{
    public interface IOutboxMessagesService
    {
        Task<OutboxMessage[]> GetByIds(params int[] ids);
        Task<OutboxMessage[]> GetUnprocessedOrderOutboxes();
        Task<bool> Insert(params OutboxMessage[] ordoutboxesrs);
        Task<bool> Update(params OutboxMessage[] outboxes);
    }
}