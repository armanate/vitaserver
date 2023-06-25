using Microsoft.EntityFrameworkCore.Diagnostics;
using System.IO.Pipelines;

namespace JWTAPI.Core.Models
{
    public class Connected
    {
        public long Id { get; set; }
        [ForeignKey("Server")]
        public long ServerId { get; set; }
        public Server Server { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }
        public DateTime Time { get; set; }
    }
    public class ConnectedPost
    {
        public long Id { get; set; }
        public long ServerId { get; set; }
        public long UserId { get; set; }
        public DateTime Time { get; set; }
    }
}
