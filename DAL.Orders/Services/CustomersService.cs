using DAL.Orders.DbModels;
using DAL.Orders.Repositories;
using DAL.Orders.Services.Base;

namespace DAL.Orders.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly CustomersRepository _repo;

        public CustomersService(AppDbContext context)
        {
            _repo = new CustomersRepository(context);
        }

        public async Task<bool> Insert(params Customer[] customers)
        {
            return await _repo.Insert(customers);
        }
        public async Task<bool> Update(params Customer[] customers)
        {
            return await _repo.Update(customers);
        }

        public async Task<Customer[]> GetByIds(params int[] ids)
        {
            return await _repo.GetByIds(ids);
        }
    }
}
