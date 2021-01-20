using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VideoStore.Domain.Seedwork
{
    public interface IRepository< TEntity > : IDisposable where TEntity : Entity
    {
        IUnitOfWork UnitOfWork { get; }

        void Add( TEntity item );
        void Modify( TEntity item );

        IEnumerable< TEntity > GetFiltered( Expression< Func< TEntity, bool > > filter );

        IEnumerable< TEntity > GetFiltered< TProperty >( Expression< Func< TEntity, bool > > filter,
                                                         params Expression< Func< TEntity, TProperty > >[] includes );
    }
}