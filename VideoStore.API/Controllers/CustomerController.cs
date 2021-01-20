using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoStore.Domain;
using VideoStore.Domain.CustomerAgg;
using VideoStore.Domain.DTO;

namespace VideoStore.API.Controllers
{
    /// <inheritdoc />
    [ ApiController ]
    [ Route( "api/[controller]" ) ]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;

        /// <inheritdoc />
        public CustomerController( ICustomerService customerService, IMapper mapper )
        {
            this.customerService = customerService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtém um cliente.
        /// </summary>
        /// <param name="id">Identificador do cliente.</param>
        /// <returns>O cliente</returns>
        [ HttpGet( "{id}" ) ]
        public IActionResult Get( int id )
        {
            try
            {
                var customer = customerService.GetCustomer( id );
                return Ok( mapper.Map< CustomerDto >( customer ) );
            }
            catch ( DomainException ex )
            {
                return StatusCode( StatusCodes.Status422UnprocessableEntity, ex.Message );
            }
            catch ( Exception ex )
            {
                Console.Write( ex.Message );
                return StatusCode( StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado." );
            }
        }

        /// <summary>
        /// Cadastra um novo cliente.
        /// </summary>
        /// <param name="dto">O cliente a ser cadastrado.</param>
        /// <returns>Um novo cliente</returns>
        /// <remarks>
        /// Exemplo de cliente para cadastrar
        ///
        ///     {
        ///         "name": "Margot Robbie",
        ///         "email": "margot.robbie@aol.com"
        ///         "avatar": "https://image.tmdb.org/t/p/w500/pbSz7d1aURy88HGzFWng5EoFU81.jpg"
        ///     }
        /// </remarks>
        [ HttpPost ]
        public IActionResult Post( CustomerDto dto )
        {
            try
            {
                var customer = customerService.AddCustomer( dto );

                return CreatedAtAction( nameof( Get ), new { id = customer.CustomerId },
                                        mapper.Map< CustomerDto >( customer ) );
            }
            catch ( DomainException ex )
            {
                return StatusCode( StatusCodes.Status422UnprocessableEntity, ex.Message );
            }
            catch ( Exception ex )
            {
                Console.Write( ex.Message );
                return StatusCode( StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado." );
            }
        }

        /// <summary>
        /// Exclui um cliente.
        /// </summary>
        /// <param name="id">Identificador do cliente.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        [ HttpDelete( "{id}" ) ]
        public IActionResult Delete( int id )
        {
            try
            {
                customerService.RemoveCustomer( id );
                return Ok();
            }
            catch ( DomainException ex )
            {
                return StatusCode( StatusCodes.Status422UnprocessableEntity, ex.Message );
            }
            catch ( Exception ex )
            {
                Console.Write( ex.Message );
                return StatusCode( StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado." );
            }
        }
    }
}