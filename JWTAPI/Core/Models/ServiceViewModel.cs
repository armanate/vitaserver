using Microsoft.Build.Execution;

namespace JWTAPI.Core.Models
{
    public class ServiceViewModel
    {
        public long Id { get; set; }
        public string ServerName { get; set; }
        public string CountryName { get; set; }
        public string Pic { get; set; }
        public int CountConnected { get; set; }
        public string ServerConfig { get; set; }

    }
}
