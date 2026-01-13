using DAL.Orders.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Orders.Repositories
{
    internal class CustomersRepository : DbRepositoryBase<Customer>
    {
        protected override DbSet<Customer> _dbSet => _context.Customers;

        public CustomersRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Customer[]> GetByEmails(params string[] emails)
        {
            return await _dbSet.Where(e => emails.Contains(e.Email)).ToArrayAsync();
        }

        public async Task<Customer[]> GetAllBy(Expression<Func<Customer, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToArrayAsync();
        }
    }
}
