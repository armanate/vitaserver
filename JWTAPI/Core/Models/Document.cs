namespace JWTAPI.Core.Models
{
    public class Document
    {
        public long Id { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }
        public string DocImage { get; set; }
        public ICollection<Payments> Payments { get; set; }

        public int AccountTypeId { get; set; }

    }
    public class DocumentPost
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string DocImage { get; set; }

        public int AccountTypeId { get; set; }

    }
}
