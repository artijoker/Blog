using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blog.Domain.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
