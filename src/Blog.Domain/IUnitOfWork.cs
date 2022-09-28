using Blog.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        ICommentRepository CommentRepository { get; }
        IRoleRepository RoleRepository { get; }
        IPostRepository PostRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IJwtTokenRepository JwtTokenRepository { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
