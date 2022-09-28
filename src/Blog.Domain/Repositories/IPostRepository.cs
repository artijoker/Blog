using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IReadOnlyList<Post>> GetPostsByStatus(string status);
        Task<IReadOnlyList<Post>> GetPostsByAccountId(int accountId);
        Task<IReadOnlyList<Post>> GetPostsByCategoryId(int categoryId);

        Task<IReadOnlyList<Post>> GetPostsByAccountIdAndStatus(int accountId, string status);
        Task<IReadOnlyList<Post>> GetPostsByCategoryIdAndStatus(int categoryId, string status);

        Task<IReadOnlyList<Post>> GetPostsByStatusAndWhichVisibleAll(string status);
        Task<IReadOnlyList<Post>> GetPostsByAccountIdAndStatusWhichVisibleAll(int accountId, string status);
        Task<IReadOnlyList<Post>> GetPostsByCategoryIdAndStatusWhichVisibleAll(int categoryId, string status);

        Task<int> GetQuantityPostsByAccountId(int accountId);
        Task<int> GetQuantityPostsByCategoryId(int categoryId);
        Task<int> GetQuantityPostsByStatus(string status);
        Task<int> GetQuantityPostsByAccountIdAndStatus(int accountId, string status);
        Task<int> GetQuantityPostsByCategoryIdAndStatus(int categoryId, string status);

        Task<IReadOnlyList<Post>> GetRecentPosts(int number);
    }
}
