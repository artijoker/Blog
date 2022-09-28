using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.HttpModels.Requests.Authentication.V1
{
    public class LogInRequestModelV1
    {
        [Required]
        public string Login { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
