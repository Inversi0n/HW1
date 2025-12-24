using DAL.Orders.DbModels;
using DAL.Orders.Repositories;

namespace DAL.Orders.Services
{
    public class OrdersService
    {
        private readonly OrdersRepository _repo;

        public OrdersService(AppDbContext context)
        {
            _repo = new OrdersRepository(context);
        }

        public async Task<bool> Insert(params Order[] orders)
        {
            return await _repo.Insert(orders);
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
