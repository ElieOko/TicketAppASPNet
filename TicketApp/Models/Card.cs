using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.Xml;

namespace TicketApp.Models
{
    [Table("TCards")]
    public class Card
    {
        public int CardId { get; set; }
        public string CardName { get; set; } = string.Empty;
    }
}