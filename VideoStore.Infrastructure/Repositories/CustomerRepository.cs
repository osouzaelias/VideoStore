using VideoStore.Domain.CustomerAgg;
using VideoStore.Infrastructure.Seedwork;

namespace VideoStore.Infrastructure.Repositories
{
    public class CustomerRepository : Repository< Customer >, ICustomerRepository
    {
        public CustomerRepository( UnitOfWork unitOfWork ) : base( unitOfWork )
        {
        }
    }
}