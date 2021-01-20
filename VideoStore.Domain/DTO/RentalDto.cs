namespace VideoStore.Domain.DTO
{
    public class RentalDto
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public bool DelayCheck { get; set; }
    }
}