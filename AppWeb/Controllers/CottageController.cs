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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CottageDto cottageDto, List<IFormFile> ImageFiles) // zmiana tutaj
        {
            if (!ModelState.IsValid)
            {
                return View(cottageDto); // i tutaj
            }

            try
            {
                await _cottageService.Create(cottageDto, ImageFiles); // i tutaj
                TempData["Success"] = "Domek został dodany pomyślnie!";
                return RedirectToAction(nameof(Create));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Błąd: " + ex.Message);
                return View(cottageDto);
            }
        }


    }
}