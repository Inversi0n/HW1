using DAL.Orders.DbModels.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Orders.DbModels
{
    [Index(nameof(Email), IsUnique = true)]
    public class Customer : DbEntityBase
    {
        public string Email { get; set; }
        public string Fullname { get; set; }
    }
}
