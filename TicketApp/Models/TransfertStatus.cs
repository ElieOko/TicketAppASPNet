using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models
{
    [Table("TTransfertStatus")]
    public class TransfertStatus
    {
        public int TransferStatusId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Transfert> transferts { get; } = new List<Transfert>();
    }
}
