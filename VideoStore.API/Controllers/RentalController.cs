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
    public class RentalController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;

        /// <inheritdoc />
        public RentalController( ICustomerService customerService, IMapper mapper )
        {
            this.customerService = customerService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Aluga um filme.
        /// </summary>
        /// <param name="dto">Dados para alugar o filme.</param>
        /// <returns>Um aluguel de filme.</returns>
        /// <remarks>
        /// Exemplo de dados para alugar um filme
        ///
        ///     {
        ///         "customerId": "5",
        ///         "movieId": "3"
        ///     }
        /// </remarks>
        [ HttpPost ]
        public IActionResult Post( RentalDto dto )
        {
            try
            {
                var rental = customerService.RentMovie( dto.CustomerId, dto.MovieId );
                return Ok( mapper.Map< RentalDto >( rental ) );
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
        /// Devolve um filme alugado.
        /// </summary>
        /// <param name="dto">Dados para devolução.</param>
        /// <returns>Os dados da devolução do filme.</returns>
        /// <remarks>
        /// Exemplo de dados para devolução do filme, onde:
        ///
        ///     {
        ///         "customerId": "5",
        ///         "movieId": "3",
        ///         "delayCheck": true
        ///     }
        /// </remarks>
        [ HttpPut ]
        public IActionResult Put( RentalDto dto )
        {
            try
            {
                var rental = customerService.GiveBackMovie( dto.CustomerId, dto.MovieId, dto.DelayCheck );
                return Ok( mapper.Map< RentalDto >( rental ) );
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