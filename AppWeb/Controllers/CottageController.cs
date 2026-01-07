using AppWeb.Application.DataTransferObject;
using AppWeb.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppWeb.MVC.Controllers
{
    public class CottageController : Controller
    {
        private readonly ICottageServices _cottageService;

        public CottageController(ICottageServices cottageService)
        {
            _cottageService = cottageService;
        }

        // Widok formularza - wyświetla stronę dodawania
        public ActionResult Create()
        {
            return View();
        }

        // Akcja obsługująca wysłanie formularza
        [HttpPost]
        public async Task<IActionResult> Create(CottageDto cottages, List<IFormFile> ImageFiles)
        {
            // Sprawdzenie, czy dane tekstowe są poprawne (walidacja)
            if (!ModelState.IsValid)
            {
                return View(cottages);
            }

            // PRZEKAZANIE DO SERWISU
            // Tutaj wysyłamy model i listę plików do CottageServices.
            // To tam teraz dzieje się zapis na dysk i dodawanie ścieżek do bazy.
            await _cottageService.Create(cottages, ImageFiles);

            // Powrót do formularza
            return RedirectToAction(nameof(Create));
        }
    }
}