using TicketApp.Models;

namespace TicketApp.Dto
{
    public class BranchDto
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public string BranchZone { get; set; } = string.Empty;
        
    }
}
