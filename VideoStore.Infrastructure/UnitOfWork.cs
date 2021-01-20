using System.Linq;
using Microsoft.EntityFrameworkCore;
using VideoStore.Domain.CustomerAgg;
using VideoStore.Domain.MovieAgg;
using VideoStore.Infrastructure.Mapping;
using VideoStore.Infrastructure.Seedwork;

namespace VideoStore.Infrastructure
{
    public class UnitOfWork : DbContext, IQueryableUnitOfWork
    {
        public virtual DbSet< Customer > Customers { get; set; }
        public virtual DbSet< Movie > Movies { get; set; }
        public virtual DbSet< Rental > Rentals { get; set; }

        public UnitOfWork()
        {
        }

        public UnitOfWork( DbContextOptions< UnitOfWork > options ) : base( options )
        {
        }

        public DbSet< TEntity > CreateSet< TEntity >() where TEntity : class
        {
            return base.Set< TEntity >();
        }

        public void SetModified< TEntity >( TEntity item ) where TEntity : class
        {
            base.Entry( item ).State = EntityState.Modified;
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public void RollbackChanges()
        {
            base.ChangeTracker
                .Entries()
                .ToList()
                .ForEach( entry => entry.State = EntityState.Unchanged );
        }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            if ( !optionsBuilder.IsConfigured )
                optionsBuilder.UseSqlServer( "Data Source=(local);Initial Catalog=DbVideoStore;Integrated Security=true" );
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.HasAnnotation( "Relational:Collation", "Latin1_General_CI_AS" );

            modelBuilder.ApplyConfiguration( new CustomerEntityTypeConfiguration() );
            modelBuilder.ApplyConfiguration( new MovieEntityTypeConfiguration() );
            modelBuilder.ApplyConfiguration( new RentalEntityTypeConfiguration() );
        }
    }
}