namespace AppWeb.Domain.Entities
{
    public class Cottage
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public CottageContactDetails ContactDetails { get; set; } = default!;

        public List<CottageImage> Images { get; set; } = new List<CottageImage>();

        public string? About { get; set; }



        public string EncodedName { get; private set; } = default!;

        public void EncodeName() => EncodedName = Name.ToLower().Replace(" ", "-");


    }
}
