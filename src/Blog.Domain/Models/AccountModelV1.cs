using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Models
{
    public class AccountModelV1
    {
        public int Id { get; set; }
        public string Login { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public DateTime Registered { get; set; }
    }
}
