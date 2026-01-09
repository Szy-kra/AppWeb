namespace AppWeb.Application.DataTransferObject
{
    public class CottageDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public int Price { get; set; } = default!;
        public int MaxPersons { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string? About { get; set; }
        public string? EncodedName { get; set; } // Zmieniłem na public set, żeby mapper mógł to wpisać


        // To jest lista "linków" (napisów), którą wypełnimy w kontrolerze
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}