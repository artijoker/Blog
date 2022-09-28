using Blog.Domain.Entities;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetByEmail(string email);
        Task<Account> GetByLogin(string login);
        Task<Account?> FindByLogin(string login);
        Task<bool> IsUniqueEmail(string email);
        Task<bool> IsUniqueLogin(string login);

        //GetAccountsAndCountByPostsForEachAccountWhereQuantityGreaterThan
        //GetAccountsAndQuantityPostsByEachHasAccountWhereCountGreaterThat
        //GetCategoriesAndCountByPostsForEachCategory
        Task<IReadOnlyList<Tuple<Account, int>>> GetAccountsAndCountPostsForEachAccountByStatus(string status);
        Task<IReadOnlyList<Tuple<Account, int>>> GetAccountsAndCountPostsForEachAccountByStatusWhereQuantityGreaterThan(string status, int quantity);
    }
}
