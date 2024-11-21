using System.ComponentModel.DataAnnotations.Schema;


namespace TicketApp.Models
{
    [Table("TTickets")]
    public class Ticket
    {
        public int TicketId { get; set; }
        public int CurrencyFId { get; set; }
        public int TransfertTypeFId { get; set; }
        public int TransfertStatusFId { get; set; }
        public int UserFId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string? Phone { get; set; } = string.Empty;
        public string? Motif { get; set; } = string.Empty;
        public string? Note { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime ClotureDateCreated { get; set; }
        public User? user{ get; set; }
        public TransfertStatus ? transfertStatus { get; set; }
        public TransferType ? transferType { get; set; }
        public Currency ? currency { get; set; }

    }
}
