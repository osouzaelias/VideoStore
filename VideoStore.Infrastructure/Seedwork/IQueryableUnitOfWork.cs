using Microsoft.EntityFrameworkCore;
using VideoStore.Domain.Seedwork;

namespace VideoStore.Infrastructure.Seedwork
{
    public interface IQueryableUnitOfWork : IUnitOfWork
    {
        DbSet< TEntity > CreateSet< TEntity >() where TEntity : class;
        void SetModified< TEntity >( TEntity item ) where TEntity : class;
    }
}