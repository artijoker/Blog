using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Models
{
    public class PostModelV1
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Anons { get; set; } = String.Empty;
        public string FullText { get; set; } = String.Empty;
        public DateTime LastChange { get; set; }
        public string Status { get; set; } = String.Empty;
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public string AccountLogin { get; set; } = String.Empty;
        public string CategoryName { get; set; } = String.Empty;

    }
}
