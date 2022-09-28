using Blog.Domain.Entities;
using Blog.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Repositories
{
    public class RoleRepository : EfRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }

        public Task<Role> GetRoleUser()
        {
            return _dbContext.Roles.FirstAsync(r => r.Name == "user");
        }

        public Task<Role> GetRoleAdmin()
        {
            return _dbContext.Roles.FirstAsync(r => r.Name == "admin");
        }

        public Task<bool> IsUniqueName(string name)
        {
            return Entities.AnyAsync(c => c.Name == name);
        }
    }
}
