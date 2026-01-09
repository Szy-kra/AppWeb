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

        public async Task<IActionResult> IndexList()
        {
            var cottageAll = await _cottageService.GetAllCottage();
            return View(cottageAll);
        }

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

            try
            {
                // Przekazujemy cottageDto, który zawiera w sobie ImageFiles
                await _cottageService.Create(cottageDto, cottageDto.ImageFiles);

                TempData["Success"] = "Domek został dodany pomyślnie!";
                return RedirectToAction(nameof(IndexList));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Wystąpił błąd podczas zapisu: " + ex.Message);
                return View(cottageDto);
            }
        }
    }
}