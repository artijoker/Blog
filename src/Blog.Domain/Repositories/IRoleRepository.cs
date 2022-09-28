using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetRoleUser();
        Task<Role> GetRoleAdmin();
        Task<bool> IsUniqueName(string name);
    }
}
