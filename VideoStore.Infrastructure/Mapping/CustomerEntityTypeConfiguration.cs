using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoStore.Domain.CustomerAgg;

namespace VideoStore.Infrastructure.Mapping
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration< Customer >
    {
        public void Configure( EntityTypeBuilder< Customer > builder )
        {
            builder.ToTable( "Customer" );

            builder.Property( e => e.CustomerId ).HasColumnName( "CustomerID" );

            builder.Property( e => e.Email )
                   .IsRequired()
                   .HasMaxLength( 80 )
                   .IsUnicode( false );

            builder.Property( e => e.ModifiedDate ).HasColumnType( "smalldatetime" );

            builder.Property( e => e.Name )
                   .IsRequired()
                   .HasMaxLength( 80 )
                   .IsUnicode( false );

            builder.Property( e => e.Avatar )
                   .IsRequired()
                   .HasMaxLength( 200 )
                   .IsUnicode( false );
        }
    }
}