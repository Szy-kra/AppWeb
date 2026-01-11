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

        // Wyświetla listę wszystkich domków (Nasze Domki)
        public async Task<IActionResult> IndexList()
        {
            var cottageAll = await _cottageService.GetAllCottage();
            return View(cottageAll);
        }

        // KROK 1: Formularz tworzenia (dane tekstowe)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CottageDto cottageDto)
        {
            if (!ModelState.IsValid)
            {
                return View(cottageDto);
            }

            await _cottageService.Create(cottageDto);

            var encodedName = cottageDto.Name.ToLower().Replace(" ", "-");

            return RedirectToAction(nameof(CreateImage), new { encodedName = encodedName });
        }

        // KROK 2: Formularz dodawania zdjęć
        [HttpGet]
        public IActionResult CreateImage(string encodedName)
        {
            ViewBag.EncodedName = encodedName;
            var cottageDto = new CottageDto { Name = encodedName.Replace("-", " ") };
            return View(cottageDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateImage(string encodedName, List<IFormFile> ImageFiles)
        {
            var imageUrls = new List<string>();

            if (ImageFiles != null && ImageFiles.Any())
            {
                foreach (var file in ImageFiles)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var pathForDatabase = "/DataImage/" + fileName;
                        imageUrls.Add(pathForDatabase);
                    }
                }
            }

            await _cottageService.AddImages(encodedName, imageUrls);

            return RedirectToAction(nameof(IndexList));
        }

        // --- PODGLĄD SZCZEGÓŁÓW (CottageMore) ---
        // Ta akcja odpowiada za wyświetlenie strony po kliknięciu "Więcej"
        [HttpGet]
        [Route("Cottage/{encodedName}/CottageMore")]
        public async Task<IActionResult> CottageMore(string encodedName)
        {
            // Wywołujemy Twoją nową nazwę metody z serwisu
            var cottageDto = await _cottageService.GetMoreForCottage(encodedName);

            if (cottageDto == null)
            {
                return NotFound();
            }

            return View(cottageDto);
        }
    }
}