namespace JWTAPI.Core.Models
{
    public class Server
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Config { get; set; }
        [ForeignKey("Country")]
        public long CountryId { get; set; }
        public Country Country { get; set; }

        public bool Status { get; set; }
        public ICollection<Connected> Connected { get; set; }

    }
    public class ServerPost
    {
        public string Name { get; set; }
        public string Config { get; set; }
        public bool Status { get; set; } = true;

    }
}
