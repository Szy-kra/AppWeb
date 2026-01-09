namespace AppWeb.Application.DataTransferObject
{
    public class CottageDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        // Zmienione na decimal, aby pasowało do Domain.Entities
        public decimal Price { get; set; }

        public int MaxPersons { get; set; }
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string? About { get; set; }
        public string? EncodedName { get; set; }

        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}