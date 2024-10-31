using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models
{
    [Table("TCalls")]
    public class Call
    {
        public int CallId { get; set; }
        public int? Ticket { get; set; }
        [Column("CounterFId")]
        public int? CounterFId { get; set; }
        public string Note { get; set; } = string.Empty;
        [Column("UserFId")]
        public int? UserFId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public User? users { get;}
        public Counter? counters { get;}
    }
}
