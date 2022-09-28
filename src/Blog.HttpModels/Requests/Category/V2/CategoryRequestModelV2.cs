using Blog.HttpModels.Requests.Category.V1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.HttpModels.Requests.Category.V2
{
    public class CategoryRequestModelV2 : CategoryRequestModelV1
    {
        [Required]
        public int CategoryId { get; set; }
    }
}
