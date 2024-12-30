//using System.ComponentModel.DataAnnotations.Schema;

//namespace TicketApp.Models
//{
//    [Table("TTransferts")]
//    public class Transfert
//    {
//        public int TransfertId { get; set; }
//        public int TransferStatusFId { get; set; }
//        public int? CallUserFId { get; set;}
//        public int BranchFId { get; set;}
//        public int UserFId { get; set;}
//        public int CurrencyFId { get; set;}
//        public int IntervalFId { get; set;}
//        public int CardFId { get; set;}
//        public int? FromBranchId { get; set;}
//        public int? ToBranchId { get; set;}
//        public double? Amount { get;set;}
//        public string? SenderName { get;set;}
//        public string? SenderPhone { get;set;}
//        public string? ReceiverName { get;set;}
//        public string? ReceiverPhone { get;set;}
//        public string? Address { get;set;}
//        public string? Note { get;set;}
//        public string? Code { get;set;}
//        public string? imagePath { get;set;}
//        public bool? Completed { get;set;}
//        public string? CompleteNote { get;set;}
//        public string? Barcode { get; set;}
//        public string? CardExpiryDate { get; set;}
//        public string? Signature { get; set;}
//        public string? TimeCalled { get; set;}
//        public DateTime DateCreated { get; set;} = DateTime.Now;
//        public string? UniqueString { get; set;}
//        public User? users { get; set;}
//        public Currency? currencies { get; set;}
//        public Card? cards { get; set;}
//        public Interval? intervals { get; set;}
//        public TransfertStatus? transfertStatus { get; set;}
//        public Branch? branches { get; set; }

//    }
//}
