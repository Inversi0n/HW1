using DAL.Orders.DbModels.Base;

namespace DAL.Orders.DbModels
{
    public class Customer : DbEntityBase
    {
        public string Email { get; set; }
        public string Fullname { get; set; }
    }
}
