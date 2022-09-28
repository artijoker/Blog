using Blog.Domain.Entities;
using Blog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.HttpModels.Responses.Authentication
{
    public class LogInResponse<TObject> : Response<TObject>
    {
        public string Token { get; set; } = string.Empty;
    }
}
