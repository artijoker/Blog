using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blog.Domain.Models
{
    public class CategoryModelV2 : CategoryModelV1
    {
        public int QuantityPosts { get; set; }
    }
}
