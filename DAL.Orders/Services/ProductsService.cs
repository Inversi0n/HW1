using DAL.Orders.DbModels;
using DAL.Orders.Repositories;
using DAL.Orders.Services.Base;

namespace DAL.Orders.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ProductsRepository _repo;

        public ProductsService(AppDbContext context)
        {
            _repo = new ProductsRepository(context);
        }

        public async Task<bool> Insert(params Product[] products)
        {
            return await _repo.Insert(products);
        }
        public async Task<bool> Update(params Product[] products)
        {
            return await _repo.Update(products);
        }

        public async Task<Product[]> GetByIds(params int[] ids)
        {
            return await _repo.GetByIds(ids);
        }
      
    }
}
