namespace AppWeb.Domain.Entities
{
    public class CottageContactDetails
    {
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
    }
}
