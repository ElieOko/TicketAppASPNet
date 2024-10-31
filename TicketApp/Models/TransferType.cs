using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models
{
    [Table("TTransferTypes")]
    public class TransferType
    {
        public int TransferTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public ICollection<Interval> intervals { get; } = new List<Interval>();
        public ICollection<OrderNumber> orderNumbers { get; } = new List<OrderNumber>();
    }
}
