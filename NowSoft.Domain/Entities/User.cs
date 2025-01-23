using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Device { get; set; } = string.Empty;
        public string IPAddress { get; set; } = string.Empty;
        public decimal Balance { get; set; } = 0; // Default balance is 0
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }


}
