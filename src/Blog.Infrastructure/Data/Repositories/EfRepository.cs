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
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly AppDbContext _dbContext;
        protected DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

        public EfRepository(AppDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual async Task Add(TEntity entity)
        {
            await Entities.AddAsync(entity);
        }

        public virtual async Task<TEntity?> FindById(int id)
        {
            return await Entities.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAll()
        {
            return await Entities.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await Entities.FirstAsync(it => it.Id == id);
        }

        public virtual Task Remove(TEntity entity)
        {
            Entities.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task UpdateRange(params TEntity[] entities)
        {
            Entities.UpdateRange(entities);
            return Task.CompletedTask;
        }
    }
}
