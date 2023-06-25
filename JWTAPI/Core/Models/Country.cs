namespace JWTAPI.Core.Models
{
    public class Country
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Pic { get; set; }
        public ICollection<Server> Server { get; set; }

    }
    public class CountryPost
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Pic { get; set; }

    }

}
