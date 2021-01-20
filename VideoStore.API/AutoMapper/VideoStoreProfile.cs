using AutoMapper;
using VideoStore.Domain.CustomerAgg;
using VideoStore.Domain.DTO;

namespace VideoStore.API.AutoMapper
{
    /// <inheritdoc />
    public class VideoStoreProfile : Profile
    {
        /// <inheritdoc />
        public VideoStoreProfile()
        {
            CreateMap< Rental, RentalDto >();
            CreateMap< Customer, CustomerDto >();
            CreateMap< CustomerDto, Customer >();
        }
    }
}