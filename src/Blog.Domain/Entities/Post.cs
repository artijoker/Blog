using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blog.Domain.Entities
{

    public static class PostStatus
    {
        public const string Draft = "Draft";
        public const string Pending = "Pending";
        public const string Publish = "Publish";
        public const string Rejected = "Rejected";
    }

    public class Post : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Anons { get; set; } = null!;
        public string FullText { get; set; } = null!;
        public int Views { get; set; } = 0;
        public DateTime LastChange { get; set; }
        public bool IsVisibleAll { get; set; }
        public bool IsAllowCommenting { get; set; }
        public string Status { get; set; } = null!;

        public int AccountId { get; set; }
        public int CategoryId { get; set; }

        
        public Account? Account { get; set; }
        public Category? Сategory { get; set; }

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
