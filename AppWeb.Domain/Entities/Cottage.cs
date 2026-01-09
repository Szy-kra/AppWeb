namespace AppWeb.Domain.Entities
{
    public class Cottage
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? About { get; set; }
        public CottageDetails ContactDetails { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? EncodedName { get; private set; }

        // Relacja 1 do wielu
        public List<CottageImage> Images { get; set; } = new List<CottageImage>();

        public void EncodeName()
        {
            if (!string.IsNullOrEmpty(Name))
                EncodedName = Name.ToLower().Replace(" ", "-");
        }
    }
}