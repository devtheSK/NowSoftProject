using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Domain.Entities
{
    public class UserLoginHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public string IpAddress { get; set; } = string.Empty;
        public string Device { get; set; } = string.Empty;
        public string Browser { get; set; } = string.Empty;
        public DateTime LoginTime { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!; 
    }
}
