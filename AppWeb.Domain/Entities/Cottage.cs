namespace AppWeb.Domain.Entities
{
    public class Cottage
    {
        // 1. Podstawowe ID
        public int Id { get; set; }

        // 2. Główne informacje
        public string Name { get; set; } = default!;

        // Długi opis (ten z niebieskiej ramki w formularzu)
        public string? About { get; set; }

        // 3. Detale (Krótki opis, cena decimal, adres)
        // Inicjalizacja = new() zapobiega crashom serwera (NullReferenceException)
        public CottageDetails ContactDetails { get; set; } = new();

        // 4. Metadane i SEO
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? EncodedName { get; private set; }

        // 5. Relacja 1 do wielu ze zdjęciami
        public List<CottageImage> Images { get; set; } = new List<CottageImage>();

        // Metoda do generowania sluga URL
        public void EncodeName() => EncodedName = Name?.ToLower().Replace(" ", "-");
    }
}