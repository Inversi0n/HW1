using DAL.Orders.DbModels.Base;

namespace DAL.Orders.DbModels
{
    public class Product : DbEntityBase
    {
        public int Price { get; set; }
        public int Name { get; set; }
    }
}
