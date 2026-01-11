namespace AppWeb.Domain.Entities
{
    public class Cottage
    {
        // 1. Podstawowe ID
        public int Id { get; set; }

        // 2. Główne informacje
        public string Name { get; set; } = default!;

        // Długi opis
        public string? About { get; set; }

        // 3. Detale (Krótki opis, cena decimal, adres)
        public CottageDetails ContactDetails { get; set; } = new();

        // 4. Metadane i SEO
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // POPRAWIONE: Zmieniono z private set na set, aby umożliwić przypisanie z serwisu
        public string? EncodedName { get; set; }

        // 5. Relacja 1 do wielu ze zdjęciami
        public List<CottageImage> Images { get; set; } = new List<CottageImage>();

        // Metoda do generowania sluga URL
        public void EncodeName() => EncodedName = Name?.ToLower().Replace(" ", "-");
    }
}