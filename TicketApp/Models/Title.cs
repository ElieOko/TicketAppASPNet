using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models
{
    [Table("TTitles")]
    public class Title
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public ICollection<Customer> customers { get; set; } = new List<Customer>();
    }
}
