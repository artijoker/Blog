using System;
using System.Collections.Generic;

namespace Blog.Domain.Entities
{
    public class Comment : IEntity
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;
        public DateTime Sent { get; set; }
        public int AccountId { get; set; }
        public int PostId { get; set; }


        public Account? Account { get; set; }
        public Post? Post { get; set; }
    }
}
