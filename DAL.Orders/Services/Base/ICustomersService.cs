using DAL.Orders.DbModels;

namespace DAL.Orders.Services.Base
{
    public interface ICustomersService
    {
        Task<Customer[]> GetByIds(params int[] ids);
        Task<bool> Insert(params Customer[] customers);
        Task<bool> Update(params Customer[] customers);
    }
}