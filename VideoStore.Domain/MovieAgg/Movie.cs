using System;
using System.Collections.Generic;
using System.Linq;
using VideoStore.Domain.CustomerAgg;
using VideoStore.Domain.Seedwork;

namespace VideoStore.Domain.MovieAgg
{
    public class Movie : Entity
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Poster { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime ModifiedDate { get; set; }

        public List< Rental > Rentals { get; set; }

        public Movie()
        {
            Rentals = new List< Rental >();
        }

        public bool IsAvailable()
        {
            return Rentals.All( rental => rental.ReturnDate != null && rental.ReturnDate < DateTime.Now );
        }

        public bool DevolutionIsDelayed( int customerId )
        {
            var rental = Rentals.SingleOrDefault( aRental => aRental.CustomerId == customerId && aRental.ReturnDate == null );
            if ( rental == null )
                throw new DomainException( "Este filme já foi devolvido." );

            return DateTime.Now > rental.DueDate;
        }
    }
}