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
        public string? AccessToken { get; set; }
        public int? ExpiresIn { get; set; }
        public string? UserSalt { get; set; }
        public Branch? branch { get; set; }
        public ICollection<Ticket> tickets { get; } = new List<Ticket>();
        public ICollection<Call> calls { get;} = new List<Call>();
    }
    public class UserLoginModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? AccessToken { get; set; }
        public int? ExpiresIn { get; set; }
        public string? UserSalt { get; set; }
    }
    public class ResetPasswordModel
    {
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }

}
