using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Models
{
    public class AccountModelV2 : AccountModelV1
    {
        public int QuantityPosts { get; set; }
    }
}
