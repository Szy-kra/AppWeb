using Microsoft.AspNetCore.Http;

namespace AppWeb.Application.DataTransferObject
{
    public class CottageDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? About { get; set; }
        public decimal Price { get; set; }
        public int MaxPersons { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }

        // Pliki wysyłane z formularza (IFormFile potrzebuje using Microsoft.AspNetCore.Http)
        public List<IFormFile>? ImageFiles { get; set; }

        // Ścieżki tekstowe dla widoków (to co idzie do bazy i z bazy)
        public List<string> ImageUrls { get; set; } = new List<string>();
    }

    // Dodatkowa klasa, żeby przestało "palić się" w MappingProfile
    public class CottageImageDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = default!;
    }
}