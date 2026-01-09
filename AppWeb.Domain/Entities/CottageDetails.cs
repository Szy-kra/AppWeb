namespace AppWeb.Domain.Entities
{
    public class CottageDetails
    {
        public string? Description { get; set; } // Krótki opis do listy
        public decimal Price { get; set; }        // Cena jako liczba dziesiętna
        public int MaxPersons { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
    }
}