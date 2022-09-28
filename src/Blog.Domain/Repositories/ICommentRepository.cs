using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {

        Task<IReadOnlyList<Comment>> GetCommentsByAccountId(int accountId);
        Task<IReadOnlyList<Comment>> GetCommentsByPostId(int postId);
        Task<Comment> GetCommentsByAccountIdAndPostId(int accountId, int postId);
    }
}
