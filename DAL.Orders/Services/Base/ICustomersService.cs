using DAL.Orders.DbModels;

namespace DAL.Orders.Services.Base
{
    public interface ICustomersService
    {
        Task<Customer[]> GetByIds(params int[] ids);
        Task<bool> Insert(params Customer[] customers);
        Task<bool> Update(params Customer[] customers);
        Task<IDictionary<int, Customer>> GetCustomersByOrderId(params int[] orderIds);
        Task<Customer[]> GetByEmail(params string[] emails);

    }
}