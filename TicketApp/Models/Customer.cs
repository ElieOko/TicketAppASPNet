using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models
{
    [Table("TCustomers")]
    public class Customer
    {
        public int CustomerId { get; set; }
        public int TitleFId { get; set; }
        public int? CardTypeFID { get; set; }
        public string? FirstName { get; set; }
        public string? lastName { get; set; }
        public string? fatherName { get; set; }
        public string? motherName { get; set; }
        public string? phoneNumber1 { get; set; }
        public string? phoneNumber2 { get; set; }
        public string? email { get; set; }
        public string? whatsappNumber { get; set; }
        public string? street { get; set; }
        public string? city { get; set; }
        public string? township { get; set; }
        public string? idCardNumber1 { get; set; }
        public string? idCardExpiryDate1 { get; set; }
        public string? signature { get; set; }
        public Title? titles { get;}
    }
}
