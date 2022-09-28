using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.HttpModels.Responses
{
    public class Response<TObject>
    {
        public bool Succeeded { get; set; }
        public bool Bug { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public TObject? Result { get; set; }
    }
}
