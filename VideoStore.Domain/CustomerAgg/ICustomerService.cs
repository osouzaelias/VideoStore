using VideoStore.Domain.DTO;

namespace VideoStore.Domain.CustomerAgg
{
    public interface ICustomerService
    {
        Rental RentMovie( int customerId, int movieId );
        Rental GiveBackMovie( int customerId, int movieId, bool delayCheck = false );
        Customer AddCustomer( CustomerDto customerDto );
        void RemoveCustomer( int customerId );
        Customer GetCustomer( int customerId );
    }
}