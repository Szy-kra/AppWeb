using AppWeb.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppWeb.MVC.Controllers
{
    public class CottageController : Controller
    {
        //private readonly przypisanie konkretnej instacnji z kodu AddScoped
        private readonly ICottageServices _cottageService;
        public CottageController(ICottageServices cottageService)
        {
            _cottageService = cottageService;
        }


        // definiowanie akcji która bedzie zwracała widok  przez nasz kontroler
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]




        public async Task<IActionResult> Create(Domain.Entities.Cottage cottages, List<IFormFile> ImageFiles)
        {

            // 1. Sprawdzamy, czy użytkownik dodał jakieś zdjęcia
            if (ImageFiles != null && ImageFiles.Any())
            {
                // Ograniczamy do 6 zdjęć na zaliczenie, żeby nie przesadzić
                foreach (var file in ImageFiles.Take(6))
                {
                    // Tworzymy unikalną nazwę pliku, żeby się nie powtarzały
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // Ścieżka do PANIEGO folderu wwwroot/uploadsImg
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploadsImg");

                    // Tworzymy folder, jeśli go jeszcze nie ma
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    var fullPath = Path.Combine(uploadPath, fileName);

                    // Kopiujemy plik z przeglądarki na PANIEGO dysk
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Dodajemy informację o zdjęciu (link do root) do obiektu domku
                    // Dzięki temu EF SQL Express zapisze to w tabeli CottageImages
                    cottages.Images.Add(new Domain.Entities.CottageImage
                    {
                        Url = "/uploadsImg/" + fileName
                    });
                }
            }





            // 2. Zapisujemy wszystko (domek + linki do zdjęć) jednym wywołaniem serwisu
            await _cottageService.Create(cottages);



            // 3. Po sukcesie przekierowujemy np. do widoku Create (lub Index)
            return RedirectToAction(nameof(Create));
        }

    }
}
