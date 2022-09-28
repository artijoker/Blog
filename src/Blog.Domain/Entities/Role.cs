using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blog.Domain.Entities
{
    public class Role : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        [JsonIgnore]
        public ICollection<Account> Accounts { get; set; } = new HashSet<Account>();
    }
}
