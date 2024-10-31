using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models
{
    [Table("TOrderNumbers")]
    public class OrderNumber
    {
        public int OrderNumberId { get; set; }
        public int Number { get; set; }
        [Column("TransferTypeFId")]
        public int TransferTypeFId { get; set; }
        [Column("BranchFId")]
        public int BranchFId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Branch? branches { get; set; }
        public TransferType? transferTypes { get; set; }
    }
}
