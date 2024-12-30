namespace TicketApp.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public bool Locked { get; set; } = false;
        public byte? AccessLevel { get; set; }
        public int? MaxAttempt { get; set; }
    }
}
