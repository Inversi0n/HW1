using DAL.Orders.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DAL.Orders.Repositories
{
    internal abstract class DbRepositoryBase<TEntity> where TEntity : DbEntityBase
    {
        protected readonly AppDbContext _context;
        protected abstract DbSet<TEntity> _dbSet { get; }
        internal DbRepositoryBase(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Insert(params TEntity[] objects)
        {
            await _dbSet.AddRangeAsync(objects);
            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task<bool> Update(params TEntity[] objects)
        {
            if (!(objects?.Length > 0))
                return false;

            List<TEntity> entities = new List<TEntity>(objects.Length);
            foreach (var obj in objects)
            {
                var oldEntity = await _dbSet.FirstOrDefaultAsync(x => x.Id == obj.Id);
                if (oldEntity == null)
                    return false;
            }

            for (int i = 0; i < objects.Length; i++)
            {
                TEntity? order = objects[i];
                var oldEntity = entities[i];

                var entry = _dbSet.Entry(oldEntity);
                entry.CurrentValues.SetValues(order);
            }
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<TEntity[]> GetByIds(params int[] ids)
        {
            return await _dbSet.Where(e => ids.Contains(e.Id)).ToArrayAsync();
        }

    }
}
