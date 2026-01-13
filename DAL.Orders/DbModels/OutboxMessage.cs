using DAL.Orders.DbModels.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Orders.DbModels
{
    [Index(nameof(MessageType), nameof(ProcessedAt))]
    public class OutboxMessage : DbEntityBase
    {
        public int MessageType { get; set; }
        public string Payload { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
    }
}
