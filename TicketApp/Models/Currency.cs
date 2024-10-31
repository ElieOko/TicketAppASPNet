using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models
{
    [Table("TCurrencies")]
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; } = string.Empty;
        public string CurrencyCode { get; set; } = string.Empty;
        public ICollection<Transfert> transferts { get; } = new List<Transfert>();
        public ICollection<Interval> intervals { get; } = new List<Interval>();

    }
}
