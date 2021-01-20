using System;

namespace VideoStore.Domain.DTO
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public bool Active { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}