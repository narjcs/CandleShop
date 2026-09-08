

using EShop.Data.Context;
using EShop.Data.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            this._dbSet = _dbContext.Set<TEntity>();
        }
        public async ValueTask DisposeAsync()
        {
            if (_dbContext != null)
            {
                await _dbContext.DisposeAsync();
            }
        }
        public IQueryable<TEntity> GetQuery()
        {
            return _dbSet.AsQueryable();
        }
        public async Task<TEntity?> GetEntityById(long id)
        {
            return await _dbSet.SingleOrDefaultAsync(d => d.Id == id);
        }
        public async Task AddEntity(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.LastUpdateDate = DateTime.Now;
            await _dbSet.AddAsync(entity);
        }
        public async Task AddRangeEntities(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreateDate = DateTime.Now;
                entity.LastUpdateDate = DateTime.Now;
            }
            await _dbSet.AddRangeAsync(entities);
        }
        public void EditEntity(TEntity entity)
        {
            entity.LastUpdateDate = DateTime.Now;
            _dbSet.Update(entity);
        }
        public void DeleteEntity(TEntity entity)
        {
            entity.IsDeleted = true;
            EditEntity(entity);
        }
        public void DeleteEntities(List<TEntity> entities)
        {
            foreach (var item in entities)
            {
                item.IsDeleted = true;
                EditEntity(item);
            }
        }
        public void DeletePermanentEntities(List<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        public void DeletePermanent(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}