using System.Linq;

namespace VideoStore.Domain.MovieAgg
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;

        public MovieService( IMovieRepository movieRepository )
        {
            this.movieRepository = movieRepository;
        }

        public Movie GetMovie( int movieId )
        {
            var movie = movieRepository.GetFiltered( entity => entity.MovieId == movieId,
                                                     include => include.Rentals )
                                       .SingleOrDefault();
            if ( movie == null )
                throw new DomainException( "O filme não existe." );

            return movie;
        }
    }
}