using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models
{
    [Table("TUsers")]
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public bool Locked { get; set; } = false;
        public byte? AccessLevel { get; set; }
        public int? MaxAttempt { get; set; }
        [Column("BranchFId")]
        public int BranchFId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool isAdmin { get; set; } = false;
        public Branch? branch { get; set; }
        public ICollection<Transfert> transferts { get;} = new List<Transfert>();
        public ICollection<Call> calls { get;} = new List<Call>();
    }
}
