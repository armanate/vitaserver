using System.Reflection.PortableExecutable;
using System.Xml.Linq;

namespace JWTAPI.Core.Models
{
    public class AccountType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Duration { get; set; }
        public string ServerList { get; set; }

        public ICollection<Payments> Payments { get; set; }

        [NotMapped]
        public long[] ServerListLong {
            get
            {
                if (ServerList != null && ServerList.Length > 0)
                    return Array.ConvertAll(ServerList.Split(';'), long.Parse);
                else
                    return null;
            }
            set
            {
                var _serverList = value;
                ServerList = String.Join(";", _serverList.Select(p => p.ToString()).ToArray());
            }
        }
    }

    public class AccounTypePost
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Duration { get; set; }
        public string ServerList { get; set; }
        [NotMapped]
        public long[] ServerListLong
        {
            get
            {
                return Array.ConvertAll(ServerList.Split(';'), long.Parse);
            }
            set
            {
                var _serverList = value;
                ServerList = String.Join(";", _serverList.Select(p => p.ToString()).ToArray());
            }
        }
    }
    

}
