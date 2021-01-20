using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VideoStore.Domain.Seedwork;

namespace VideoStore.Infrastructure.Seedwork
{
    public class Repository< TEntity > : IRepository< TEntity > where TEntity : Entity
    {
        private readonly IQueryableUnitOfWork unitOfWork;

        public Repository( IQueryableUnitOfWork unitOfWork )
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException( nameof( unitOfWork ) );
        }

        public IUnitOfWork UnitOfWork => unitOfWork;

        public virtual void Add( TEntity item )
        {
            if ( item != null )
                GetSet().Add( item );
        }

        public virtual void Modify( TEntity item )
        {
            if ( item != null )
                unitOfWork.SetModified( item );
        }

        public IEnumerable< TEntity > GetFiltered( Expression< Func< TEntity, bool > > filter )
        {
            return GetSet().Where( filter );
        }

        public IEnumerable< TEntity > GetFiltered< TProperty >( Expression< Func< TEntity, bool > > filter, params Expression< Func< TEntity, TProperty > >[] includes )
        {
            var query = GetSet().Where( filter );

            if ( includes.Length > 0 )
                query = includes.Aggregate( query, ( current, include ) => current.Include( include ) );

            return query;
        }

        public void Dispose()
        {
            unitOfWork?.Dispose();
        }

        private DbSet< TEntity > GetSet()
        {
            return unitOfWork.CreateSet< TEntity >();
        }
    }
}