using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Models
{
    public  class AccountModelV3 : AccountModelV2
    {
        public int roleId { get; set; }
        public string roleName { get; set; } = string.Empty;
        public bool IsBanned { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
