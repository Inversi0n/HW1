using DAL.Orders.DbModels;

namespace DAL.Orders.Services.Base
{
    public interface IProductsService
    {
        Task<Product[]> GetByIds(params int[] ids);
        Task<bool> Insert(params Product[] products);
        Task<bool> Update(params Product[] products);
    }
}