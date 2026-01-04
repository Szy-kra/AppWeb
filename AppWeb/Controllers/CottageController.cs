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

        [HttpPost]

        public async Task<IActionResult> Create(Domain.Entities.Cottage cottages)
        {
            await _cottageService.Create(cottages);
            return RedirectToAction(nameof(Create));  //  <--REFACTOR

        }

    }
}
