using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoStore.Domain.CustomerAgg;

namespace VideoStore.Infrastructure.Mapping
{
    public class RentalEntityTypeConfiguration : IEntityTypeConfiguration< Rental >
    {
        public void Configure( EntityTypeBuilder< Rental > builder )
        {
            builder.ToTable( "Rental" );

            builder.HasIndex( e => new { e.CustomerId, e.MovieId }, "UIX_CustomerID_MovieID" )
                   .IsUnique();

            builder.Property( e => e.RentalId ).HasColumnName( "RentalID" );

            builder.Property( e => e.CustomerId ).HasColumnName( "CustomerID" );

            builder.Property( e => e.ModifiedDate ).HasColumnType( "smalldatetime" );

            builder.Property( e => e.MovieId ).HasColumnName( "MovieID" );

            builder.Property( e => e.RentalDate ).HasColumnType( "smalldatetime" );

            builder.Property( e => e.ReturnDate ).HasColumnType( "smalldatetime" );

            builder.HasOne( d => d.Customer )
                   .WithMany( p => p.Rentals )
                   .HasForeignKey( d => d.CustomerId )
                   .OnDelete( DeleteBehavior.ClientSetNull )
                   .HasConstraintName( "FK_CustomerID" );

            builder.HasOne( d => d.Movie )
                   .WithMany( p => p.Rentals )
                   .HasForeignKey( d => d.MovieId )
                   .OnDelete( DeleteBehavior.ClientSetNull )
                   .HasConstraintName( "FK_MovieID" );
        }
    }
}