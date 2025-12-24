using DAL.Orders.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DAL.Orders.Repositories
{
    internal class OrdersRepository : DbRepositoryBase<Order>
    {
        protected override DbSet<Order> _dbSet => _context.Orders;

        public OrdersRepository(AppDbContext context) : base(context)
        {
        }
    }
}
