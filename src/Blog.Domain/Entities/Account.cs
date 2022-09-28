using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blog.Domain.Entities
{
    public class Account : IEntity
    {
        public int Id { get; set; }

        public string Login { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime Registered { get; set; }
        public bool IsBanned { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public int RoleId { get; set; }

        public Role? Role { get; set; }
        [JsonIgnore]
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        [JsonIgnore]
        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();

        [JsonIgnore]
        public ICollection<JwtToken> Tokens { get; set; } = new HashSet<JwtToken>();

    }
}
