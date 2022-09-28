using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.HttpModels.Requests.Post.V1;

namespace Blog.HttpModels.Requests.Post.V2
{
    public class PostRequestsModelV2 : PostRequestsModelV1
    {
        public int PostId { get; set; }
    }
}
