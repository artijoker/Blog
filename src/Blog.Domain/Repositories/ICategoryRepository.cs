using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> IsUniqueName(string name);

        Task<IReadOnlyList<Tuple<Category, int>>> GetCategoriesAndCountPostsForEachCategoryByStatys(string status);
    }
}
