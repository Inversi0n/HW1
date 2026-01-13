using DAL.Orders.DbModels.Base;

namespace DAL.Orders.DbModels
{
    public class Order : DbEntityBase
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public DateTime BoughtAt { get; set; }
        public DateTime? ConfirmedAt { get; set; }
    }
}
