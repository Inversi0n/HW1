using DAL.Orders.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DAL.Orders.Repositories
{
    internal class OutboxMessagesRepository : DbRepositoryBase<OutboxMessage>
    {
        protected override DbSet<OutboxMessage> _dbSet => _context.OutboxMessages;

        public OutboxMessagesRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<OutboxMessage[]> GetUnprocessed(int outboxType, int maxCount)
        {
            return await _dbSet
                .Where(e => e.MessageType == outboxType && e.ProcessedAt == null)
                .OrderBy(e => e.CreatedAt)
                .Take(maxCount)
                .ToArrayAsync();
        }
    }
}
