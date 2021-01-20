using System;

namespace VideoStore.Domain.Seedwork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void RollbackChanges();
    }
}