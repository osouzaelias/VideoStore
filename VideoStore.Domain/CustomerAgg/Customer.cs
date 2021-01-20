using System;
using System.Collections.Generic;
using VideoStore.Domain.MovieAgg;
using VideoStore.Domain.Seedwork;

namespace VideoStore.Domain.CustomerAgg
{
    public class Customer : Entity
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public bool Active { get; set; }
        public DateTime ModifiedDate { get; set; }

        public List< Rental > Rentals { get; set; }

        public Customer()
        {
            Rentals = new List< Rental >();
        }

        /// <summary>
        ///     Alugar um filme.
        /// </summary>
        /// <param name="movie">O filme a ser alugado.</param>
        public void RentMovie( Movie movie )
        {
            if ( movie.IsAvailable() )
                Rentals.Add( new Rental( CustomerId, movie.MovieId ) );
            else
                throw new DomainException( "O filme não está disponível." );
        }

        /// <summary>
        ///     Devolver um filme que estava alugado.
        /// </summary>
        /// <param name="movie">O filme a ser devolvido.</param>
        public void GiveBackMovieWithDelayCheck( Movie movie )
        {
            if ( movie.DevolutionIsDelayed( CustomerId ) )
                throw new DomainException( "A devolução do filme está com atraso." );

            GiveBackMovie( movie );
        }

        /// <summary>
        ///     Devolver com atraso um filme que estava alugado.
        /// </summary>
        /// <param name="movie">O filme a ser devolvido.</param>
        public void GiveBackMovie( Movie movie )
        {
            Rentals.ForEach( rental =>
            {
                if ( rental.MovieId == movie.MovieId && rental.ReturnDate == null )
                    rental.ReturnDate = DateTime.Now;
            } );
        }
    }
}