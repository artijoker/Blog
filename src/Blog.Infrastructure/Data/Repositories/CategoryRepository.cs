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
    public class CategoryRepository : EfRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Tuple<Category, int>>> GetCategoriesAndCountPostsForEachCategoryByStatys(string status)
        {
            return await Entities.Select(c => new Tuple<Category, int>(c, c.Posts.Where(p => p.Status == status).Count())).ToListAsync();
        }

        public Task<bool> IsUniqueName(string name)
        {
            return Entities.AnyAsync(c => c.Name == name);
        }
    }
}
