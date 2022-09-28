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
    public class PostRepository : EfRepository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Post?> FindById(int id)
        {
            return await Entities.Where(p => p.Id == id)
                .Include(p => p.Account)
                .Include(p => p.Сategory)
                //.Include(p => p.Comments)
                .FirstOrDefaultAsync();
        }

        public override async Task<IReadOnlyList<Post>> GetAll()
        {
            return await Entities.Include(p => p.Account)
               .Include(p => p.Сategory)
                .ToListAsync();
        }

        public override async Task<Post> GetById(int id)
        {
            return await Entities.Where(p => p.Id == id)
                .Include(p => p.Account)
                .Include(p => p.Сategory)
                //.Include(p => p.Comments)
                //    .ThenInclude(c => c.Account)
                .FirstAsync(it => it.Id == id);
        }

        public async Task<IReadOnlyList<Post>> GetPostsByAccountIdAndStatus(int accountId, string status)
        {
            return await Entities.Where(p => p.AccountId == accountId && p.Status == status)
                .Include(p => p.Account)
                .Include(p => p.Сategory)
                .ToListAsync();
        }



        public async Task<IReadOnlyList<Post>> GetPostsByCategoryIdAndStatus(int categoryId, string status)
        {
            return await Entities.Where(p => p.CategoryId == categoryId && p.Status == status)
                .Include(p => p.Account)
                .Include(p => p.Сategory)
                .ToListAsync();
        }



        public async Task<IReadOnlyList<Post>> GetPostsByStatus(string status)
        {
            return await Entities.Where(p => p.Status == status)
                .Include(p => p.Account)
                .Include(p => p.Сategory)
                .ToListAsync();
        }
        //GetPostsByAccountIdAndStatusWhichVisibleAll
        public async Task<IReadOnlyList<Post>> GetPostsByStatusAndWhichVisibleAll(string status)
        {
            return await Entities.Where(p => p.Status == status && p.IsVisibleAll == true)
               .Include(p => p.Account)
               .Include(p => p.Сategory)
               .ToListAsync();
        }

        public async Task<IReadOnlyList<Post>> GetPostsByAccountIdAndStatusWhichVisibleAll(int accountId, string status)
        {
            return await Entities.Where(p => p.AccountId == accountId && p.Status == status && p.IsVisibleAll == true)
               .Include(p => p.Account)
               .Include(p => p.Сategory)
               .ToListAsync();
        }
        public async Task<IReadOnlyList<Post>> GetPostsByCategoryIdAndStatusWhichVisibleAll(int categoryId, string status)
        {
            return await Entities.Where(p => p.CategoryId == categoryId && p.Status == status && p.IsVisibleAll == true)
               .Include(p => p.Account)
               .Include(p => p.Сategory)
               .ToListAsync();
        }

        public async Task<IReadOnlyList<Post>> GetPostsByAccountId(int accountId)
        {
            return await Entities.Where(p => p.AccountId == accountId)
                .Include(p => p.Account)
               .Include(p => p.Сategory)
               .ToListAsync();
        }

        public async Task<IReadOnlyList<Post>> GetPostsByCategoryId(int categoryId)
        {
            return await Entities.Where(p => p.CategoryId == categoryId)
               .Include(p => p.Account)
               .Include(p => p.Сategory)
               .ToListAsync();
        }

        public Task<int> GetQuantityPostsByAccountId(int accountId)
        {
            return Entities.CountAsync(p => p.AccountId == accountId);
        }

        public Task<int> GetQuantityPostsByCategoryId(int categoryId)
        {
            return Entities.CountAsync(p => p.CategoryId == categoryId);
        }

        public Task<int> GetQuantityPostsByStatus(string status)
        {
            return Entities.CountAsync(p => p.Status == status);
        }

        public Task<int> GetQuantityPostsByAccountIdAndStatus(int accountId, string status)
        {
            return Entities.CountAsync(p => p.AccountId == accountId && p.Status == status);
        }

        public Task<int> GetQuantityPostsByCategoryIdAndStatus(int categoryId, string status)
        {
            return Entities.CountAsync(p => p.CategoryId == categoryId && p.Status == status);
        }

        public async Task<IReadOnlyList<Post>> GetRecentPosts(int number)
        {
            return await Entities.OrderByDescending(p => p.Id)
                .Where(p => p.Status == PostStatus.Publish)
                .Take(number)
                .Include(p => p.Account)
                .Include(p => p.Сategory)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Post>> GetRecentPostsWithStatus(int number, string status)
        {
            return await Entities.OrderByDescending(p => p.Id)
                .Where(p => p.Status == status)
                .Take(number)
                .Include(p => p.Account)
                .Include(p => p.Сategory)
                .ToListAsync();
        }
    }
}
