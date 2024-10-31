using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models
{
    [Table("TIntervals")]
    public class Interval
    {
        public int IntervalId { get; set; }
        [Column("TransferTypeFId")]
        public int? TransferTypeFId { get; set; }
        [Column("CurrencyFId")]
        public int? CurrencyFId { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
        public Currency? currencies { get; set; }
        public TransferType? transferTypes { get; set; }
        public ICollection<Transfert> transferts { get; } = new List<Transfert>();
    }
}
