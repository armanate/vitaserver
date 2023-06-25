namespace JWTAPI.Core.Models
{
    public class Payments
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public long AccountTypeId { get; set; }
        [ForeignKey("AccountTypeId")]

        public AccountType AccountType { get; set; }

        public long DocumentId { get; set; }
        [ForeignKey("DocumentId")]
        public Document Document { get; set; }

        public DateTime PaymentDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
    public class PaymentsPost
    {
        public string Phone { get; set; }
        public int AccessDays { get; set; }
    }
}
