using Blog.HttpModels.Requests.Account.V1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.HttpModels.Requests.Account.V2
{
    public class AccountUpdateRequestModelV2 : AccountUpdateRequestModelV1
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
