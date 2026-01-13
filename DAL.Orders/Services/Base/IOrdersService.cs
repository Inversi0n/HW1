using DAL.Orders.DbModels;

namespace DAL.Orders.Services.Base
{
    public interface IOrdersService
    {
        Task<Order[]> GetByIds(params int[] ids);
        //Task<bool> Insert(params Order[] orders);
        Task<bool> CreateOrder(Order order);
        Task<bool> Update(params Order[] orders);
    }
}