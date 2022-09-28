using Blog.Domain;
using Blog.Domain.Repositories;

namespace Blog.Infrastructure.Data
{
    public class UnitOfWorkEf : IUnitOfWork, IAsyncDisposable
    {
        private readonly AppDbContext _dbContext;

        public IAccountRepository AccountRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IPostRepository PostRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IJwtTokenRepository JwtTokenRepository { get; }

        public UnitOfWorkEf(
            AppDbContext dbContext,
            IAccountRepository accountRepository,
            ICommentRepository commentRepository,
            IRoleRepository roleRepository,
            IPostRepository postRepository,
            ICategoryRepository categoryRepository,
            IJwtTokenRepository jwtTokenRepository
            )
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            AccountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            CommentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            RoleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            PostRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            CategoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            JwtTokenRepository = jwtTokenRepository ?? throw new ArgumentNullException(nameof(jwtTokenRepository));
        }

        public void Dispose() => _dbContext.Dispose();

        public ValueTask DisposeAsync() => _dbContext.DisposeAsync();

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
            => _dbContext.SaveChangesAsync(cancellationToken);
    }
}
