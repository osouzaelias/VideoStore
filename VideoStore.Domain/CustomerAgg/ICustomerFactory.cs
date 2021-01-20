namespace VideoStore.Domain.CustomerAgg
{
    public interface ICustomerFactory
    {
        Customer Create( string name, string email, string avatar );
    }
}