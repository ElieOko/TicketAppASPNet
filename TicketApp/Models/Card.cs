using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models
{
    [Table("TCards")]
    public class Card
    {
        public int CardId { get; set; }
        public string CardName { get; set; } = string.Empty;
        public ICollection<Transfert> transferts { get; } = new List<Transfert>();
    }
}
