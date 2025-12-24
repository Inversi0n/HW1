using DAL.Orders.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DAL.Orders.Repositories
{
    internal class CustomersRepository : DbRepositoryBase<Customer>
    {
        protected override DbSet<Customer> _dbSet => _context.Customers;

        public CustomersRepository(AppDbContext context) : base(context)
        {
        }
    }
}
