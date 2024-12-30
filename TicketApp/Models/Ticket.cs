using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


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
        public bool StatusChanged { get; set; }
        public string? Phone { get; set; } = string.Empty;
        public string? Motif { get; set; } = string.Empty;
        public string? HeureDebut { get; set; }
        public string? HeureFin { get; set; }
        public string? DureeS { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? ClotureDateCreated { get; set; }
        public User? user{ get; set; }
        public TransfertStatus ? transfertStatus { get; set; }
        public Branch ? branch{ get; set; }
        public TransferType ? transferType { get; set; }
        [JsonIgnore]
        public Currency ? currency { get; set; }

    }
    public class ChangeStatusTicket
    {
        public int TransfertStatusFId { get; set; }
    }
}
