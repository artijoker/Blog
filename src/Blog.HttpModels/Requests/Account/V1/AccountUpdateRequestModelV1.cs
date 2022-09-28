using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.HttpModels.Requests.Account.V1
{
    public class AccountUpdateRequestModelV1
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Login { get; set; } = string.Empty;

        public string? NewPassword { get; set; }
    }
}
