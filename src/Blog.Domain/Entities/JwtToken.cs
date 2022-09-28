using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blog.Domain.Entities
{
    public class JwtToken : IEntity
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public int AccountId { get; set; }
        public bool IsBlocked { get; set; } = false;

        [JsonIgnore]
        public Account? Account { get; set; }
    }
}
