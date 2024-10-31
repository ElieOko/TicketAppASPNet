using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models
{
    [Table("TCounters")]
    public class Counter
    {
        public int CounterId { get; set; }
        public string CounterName { get; set; } = string.Empty;
        public int BranchFId { get; set; }
        public Branch? branches { get;}
        public ICollection<Call> calls { get; } = new List<Call>();
    }
}
