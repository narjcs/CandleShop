using EShop.Data.Entities.Common;
using System.Security.Cryptography;

namespace EShop.Data.Repository
{
    public interface IGenericRepository<TEntity>: IAsyncDisposable where TEntity : BaseEntity 
    {
        IQueryable<TEntity> GetQuery();
        Task<TEntity?> GetEntityById(long id);
        Task AddEntity (TEntity entity);
        Task AddRangeEntities (List<TEntity> entities);
        void EditEntity (TEntity entity);
        void DeleteEntity (TEntity entity);     //Soft Delete, IsDelete = true
        void DeleteEntities (List<TEntity> entities);
        void DeletePermanentEntities(List<TEntity> entities);
        void DeletePermanent (TEntity entity);  //Hard Delete
        Task SaveAsync();

    }
}
