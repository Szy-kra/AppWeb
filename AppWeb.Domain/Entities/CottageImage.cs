//relacja jeden do wielu - jeden domek może mieć wiele zdjęć
namespace AppWeb.Domain.Entities
{
    public class CottageImage
    {
        public int Id { get; set; }
        public string Url { get; set; } = default!;

        // Klucz obcy - to pole mówi bazie, do którego domku należy to zdjęcie
        public int CottageId { get; set; }
        public Cottage Cottage { get; set; } = default!;
    }
}