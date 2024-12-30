using System.ComponentModel.DataAnnotations.Schema;
using TicketApp.Interfaces;

namespace TicketApp.Models
{
    [Table("TBranches")]
    public class Branch
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public string BranchZone { get; set; } = string.Empty;
        public ICollection<User> Users { get; } = new List<User>();
        public ICollection<Transfert> transferts { get; } = new List<Transfert>();
        public ICollection<Ticket> tickets { get; } = new List<Ticket>();
        public ICollection<Counter> counters { get; } = new List<Counter>();
        public ICollection<OrderNumber> orderNumbers { get; } = new List<OrderNumber>();

    }
}
