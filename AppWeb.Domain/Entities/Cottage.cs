using Microsoft.AspNetCore.Identity;

namespace AppWeb.Domain.Entities
{
    public class Cottage
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? About { get; set; }
        public CottageDetails ContactDetails { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? EncodedName { get; set; }

        // Klucz obcy (Id użytkownika)
        public string? CreatedById { get; set; }

        // Właściwość nawigacyjna (Obiekt użytkownika) - ZMIENIONA NAZWA NA CreatedBy
        public IdentityUser? CreatedBy { get; set; }

        public List<CottageImage> Images { get; set; } = new List<CottageImage>();

        public void EncodeName() => EncodedName = Name?.ToLower().Replace(" ", "-");
    }
}