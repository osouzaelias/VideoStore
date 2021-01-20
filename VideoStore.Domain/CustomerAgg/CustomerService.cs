using System;
using System.Linq;
using VideoStore.Domain.DTO;
using VideoStore.Domain.MovieAgg;

namespace VideoStore.Domain.CustomerAgg
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerFactory customerFactory;
        private readonly ICustomerRepository customerRepository;
        private readonly IMovieService movieService;

        public CustomerService( ICustomerRepository customerRepository,
                                IMovieService movieService,
                                ICustomerFactory customerFactory )
        {
            this.customerRepository = customerRepository;
            this.movieService = movieService;
            this.customerFactory = customerFactory;
        }

        public Rental RentMovie( int customerId, int movieId )
        {
            var customer = GetCustomer( customerId );
            customer.RentMovie( movieService.GetMovie( movieId ) );

            customerRepository.Modify( customer );
            customerRepository.UnitOfWork.Commit();

            return customer.Rentals.Last();
        }

        public Rental GiveBackMovie( int customerId, int movieId, bool delayCheck = false )
        {
            var customer = GetCustomer( customerId );

            if ( delayCheck )
                customer.GiveBackMovieWithDelayCheck( movieService.GetMovie( movieId ) );
            else
                customer.GiveBackMovie( movieService.GetMovie( movieId ) );

            customerRepository.Modify( customer );
            customerRepository.UnitOfWork.Commit();

            return customer.Rentals.Last();
        }

        public Customer AddCustomer( CustomerDto customerDto )
        {
            if ( customerDto == null )
                throw new ArgumentNullException( nameof( customerDto ) );

            var newCustomer = customerFactory.Create( customerDto.Name, customerDto.Email, customerDto.Avatar );

            if ( newCustomer.Active == false )
            {
                newCustomer.Active = true;
                customerRepository.Modify( newCustomer );
            }
            else
            {
                customerRepository.Add( newCustomer );
            }

            customerRepository.UnitOfWork.Commit();

            return newCustomer;
        }

        public void RemoveCustomer( int customerId )
        {
            if ( customerId <= 0 )
                throw new ArgumentException( nameof( customerId ) );

            var customer = GetCustomer( customerId );
            customer.Active = false;
            customer.ModifiedDate = DateTime.Now;

            customerRepository.Modify( customer );
            customerRepository.UnitOfWork.Commit();
        }

        public Customer GetCustomer( int customerId )
        {
            var customer = customerRepository.GetFiltered( entity => entity.CustomerId == customerId,
                                                           include => include.Rentals )
                                             .SingleOrDefault();
            if ( customer == null )
                throw new DomainException( "O cliente não existe." );

            return customer;
        }
    }
}