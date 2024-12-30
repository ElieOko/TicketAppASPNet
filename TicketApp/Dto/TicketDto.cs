namespace TicketApp.Dto
{
    public class TicketDto
    {
        public int TicketId { get; set; }
        public int CurrencyFId { get; set; }
        public int TransfertTypeFId { get; set; }
        public int TransfertStatusFId { get; set; }
        public int UserFId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public double Amount { get; set; }
        public bool StatusChanged { get; set; }
        public string? Phone { get; set; } = string.Empty;
        public string? Motif { get; set; } = string.Empty;
        public DateTime? DateCreated { get; set; }
        public string? HeureDebut { get; set; }
        public string? HeureFin { get; set; }
        public string? DureeS { get; set; }
        public DateTime? ClotureDateCreated { get; set; }
        public UserDto User { get; set; } = new UserDto();
        public BranchDto Branch { get; set; }=new BranchDto();
        public CurrencyDto Currency { get; set; } = new CurrencyDto();
        public TransfertStatusDto TransfertStatus { get; set; } = new TransfertStatusDto();
        public TransfertTypeDto TransferType { get; set; } = new TransfertTypeDto();
    }

}
