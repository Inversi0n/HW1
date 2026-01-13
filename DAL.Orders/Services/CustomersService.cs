using DAL.Orders.DbModels;
using DAL.Orders.Repositories;
using DAL.Orders.Services.Base;

namespace DAL.Orders.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly AppDbContext _context;
        private readonly CustomersRepository _repo;

        public CustomersService(AppDbContext context)
        {
            _context = context;
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

        public async Task<Customer[]> GetByEmail(params string[] emails)
        {
            if (emails == null || emails.Length == 0)
                return new Customer[0];

            var filteredEmails = emails.Where(e => !string.IsNullOrWhiteSpace(e)).ToArray();
            return await _repo.GetByEmails(filteredEmails);
        }

        public async Task<IDictionary<int, Customer>> GetCustomersByOrderId(params int[] orderIds)
        {
            var ordersRepo = new OrdersRepository(_context); //in the same connection

            var orders = await ordersRepo.GetByIds(orderIds);
            var customerIds = orders.Select(o => o.CustomerId).ToArray();

            var foundCustomers = await _repo.GetByIds(customerIds);
            var customersDict = foundCustomers.GroupBy(c => c.Id).ToDictionary(gr => gr.Key, gr => gr.First());//customers by its ID

            var res = orders
                .Select(o => new KeyValuePair<int, Customer>(o.Id, customersDict[o.CustomerId]))
                .ToDictionary();
            return res;
        }
    }
}
