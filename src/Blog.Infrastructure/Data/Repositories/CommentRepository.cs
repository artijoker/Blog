using Blog.Domain.Entities;
using Blog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Data.Repositories
{
    public class CommentRepository : EfRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Comment?> FindById(int id)
        {
            return await Entities.Where(p => p.Id == id).Include(p => p.Account).FirstOrDefaultAsync();
        }

        public override async Task<IReadOnlyList<Comment>> GetAll()
        {
            return await Entities.Include(p => p.Account).ToListAsync();
        }

        public override async Task<Comment> GetById(int Id)
        {
            return await Entities.Include(p => p.Account).FirstAsync(it => it.Id == Id);
        }

        public async Task<IReadOnlyList<Comment>> GetCommentsByAccountId(int accountId)
        {
            return await Entities.Where(c => c.AccountId == accountId).ToListAsync();
        }

        public async Task<IReadOnlyList<Comment>> GetCommentsByPostId(int postId)
        {
            return await Entities.Where(c => c.PostId == postId).Include(p => p.Account).ToListAsync();
        }

        public async Task<Comment> GetCommentsByAccountIdAndPostId(int accountId, int postId)
        {
            return await Entities.Where(c => c.AccountId == accountId && c.PostId == postId).FirstAsync();
        }
    }

}
