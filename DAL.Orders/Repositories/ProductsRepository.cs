using DAL.Orders.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DAL.Orders.Repositories
{
    internal class ProductsRepository : DbRepositoryBase<Product>
    {
        protected override DbSet<Product> _dbSet => _context.Products;

        public ProductsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
