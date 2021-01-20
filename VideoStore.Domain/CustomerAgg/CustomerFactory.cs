using System;
using System.Linq;

namespace VideoStore.Domain.CustomerAgg
{
    public class CustomerFactory : ICustomerFactory
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerFactory( ICustomerRepository customerRepository )
        {
            this.customerRepository = customerRepository;
        }

        public Customer Create( string name, string email, string avatar )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
                throw new DomainException( "O nome do cliente é obrigatório." );

            if ( string.IsNullOrWhiteSpace( email ) )
                throw new DomainException( "O e-mail do cliente é obrigatório." );

            if ( string.IsNullOrWhiteSpace( avatar ) )
                throw new DomainException( "O avatar do cliente é obrigatório." );

            var customer = customerRepository.GetFiltered( entity => entity.Email == email )
                                             .SingleOrDefault();

            if ( customer == null )
            {
                var newCustomer = new Customer();
                newCustomer.Name = name;
                newCustomer.Avatar = avatar;
                newCustomer.Email = email;
                newCustomer.Active = true;
                newCustomer.ModifiedDate = DateTime.Now;
                newCustomer.Rentals = null;

                return newCustomer;
            }

            if ( customer.Active )
                throw new DomainException( "Já existe um cliente com o e-mail informado." );

            return customer;
        }
    }
}