namespace TicketApp.Models
{
    public class LogEntry
    {
        public string? Action { get; set; }
        public string? UserName { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Message { get; set; }
    }
}
