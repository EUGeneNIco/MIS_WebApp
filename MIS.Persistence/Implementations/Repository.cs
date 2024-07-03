using Microsoft.EntityFrameworkCore;
using MIS.Application._Interfaces;

namespace MIS.Persistence.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext dbContext;
        private readonly DbSet<T> dbSet;

        public Repository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await this.dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            if (this.dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }
            this.dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this.dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            this.dbSet.Attach(entity);
            this.dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await this.dbContext.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<T> GetAllQuery()
        {
            return (this.dbSet).AsQueryable();
        }
    }
}
