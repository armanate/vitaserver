namespace JWTAPI.Core.Models
{
    public class ClientInfo
    {
        public int Id { get; set; }


        public int LastVersionCode { get; set; }
        public string LastVersionUrl { get; set; }

        public bool IsForce { get; set; }
    }

    public class ClientInfoPostModel
    {
        public int LastVersionCode { get; set; }
        public string LastVersionUrl { get; set; }

        public bool IsForce { get; set; }
    }
}
