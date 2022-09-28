using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.HttpModels.Requests.Post.V1
{
    public class PostRequestsModelV1
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Anons { get; set; } = string.Empty;

        [Required]
        public string FullText { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        //[Required]
        //public bool IsVisibleEveryone { get; set; }

        //[Required]
        //public bool IsAllowCommenting { get; set; }
    }
}
