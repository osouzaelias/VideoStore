using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoStore.Domain.MovieAgg;

namespace VideoStore.Infrastructure.Mapping
{
    public class MovieEntityTypeConfiguration : IEntityTypeConfiguration< Movie >
    {
        public void Configure( EntityTypeBuilder< Movie > builder )
        {
            builder.ToTable( "Movie" );

            builder.Property( e => e.MovieId ).HasColumnName( "MovieID" );

            builder.Property( e => e.Title )
                   .IsRequired()
                   .HasMaxLength( 250 )
                   .IsUnicode( false );

            builder.Property( e => e.Description )
                   .IsRequired()
                   .HasMaxLength( 800 )
                   .IsUnicode( false );

            builder.Property( e => e.Poster )
                   .IsRequired()
                   .HasMaxLength( 250 )
                   .IsUnicode( false );

            builder.Property( e => e.ModifiedDate ).HasColumnType( "smalldatetime" );
        }
    }
}