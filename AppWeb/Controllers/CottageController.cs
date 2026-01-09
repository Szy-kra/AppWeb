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

        // GET: Cottage/Create
        // Wyświetla pusty formularz
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cottage/Create
        // Obsługuje wysyłkę danych z formularza
        [HttpPost]
        [ValidateAntiForgeryToken] // Zabezpieczenie przed atakami CSRF
        public async Task<IActionResult> Create(CottageDto cottages, List<IFormFile> ImageFiles)
        {
            // 1. SPRAWDZENIE MODELU
            // Jeśli walidator (FluentValidation) znajdzie błędy, ModelState.IsValid będzie false.
            if (!ModelState.IsValid)
            {
                // Zwracamy widok z tym samym modelem 'cottages'.
                // Dzięki temu błędy pojawią się w Twoich <span> w HTML.
                return View(cottages);
            }

            // 2. LOGIKA BIZNESOWA
            // Jeśli dane są poprawne, przesyłamy je do serwisu.
            try
            {
                await _cottageService.Create(cottages, ImageFiles);

                // Możesz dodać powiadomienie o sukcesie (opcjonalnie)
                TempData["Success"] = "Domek został dodany pomyślnie!";

                // 3. PRZEKIEROWANIE
                // Po udanym zapisie czyścimy formularz przekierowując na akcję GET
                return RedirectToAction(nameof(Create));
            }
            catch (Exception ex)
            {
                // Jeśli serwis wywali błąd (np. problem z bazą), dodajemy błąd do widoku
                ModelState.AddModelError("", "Wystąpił błąd podczas zapisu: " + ex.Message);
                return View(cottages);
            }
        }
    }
}