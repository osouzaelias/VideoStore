using VideoStore.Domain.MovieAgg;
using VideoStore.Infrastructure.Seedwork;

namespace VideoStore.Infrastructure.Repositories
{
    public class MovieRepository : Repository< Movie >, IMovieRepository
    {
        public MovieRepository( UnitOfWork unitOfWork ) : base( unitOfWork )
        {
        }
    }
}