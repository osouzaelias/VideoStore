using System;
using VideoStore.Domain.MovieAgg;

namespace VideoStore.Domain.CustomerAgg
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool Active { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Customer Customer { get; set; }
        public Movie Movie { get; set; }

        public Rental( int customerId, int movieId )
        {
            CustomerId = customerId;
            MovieId = movieId;
            RentalDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            Active = true;

            CalculateDueDate();
        }

        private void CalculateDueDate()
        {
            DueDate = DateTime.Now.AddDays( 30 );
        }
    }
}