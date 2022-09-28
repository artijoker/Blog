using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.HttpModels.Requests.Category.V1
{
    public class CategoryRequestModelV1
    {

        [Required]
        public string CategoryName { get; set; } = string.Empty;
    }
}
