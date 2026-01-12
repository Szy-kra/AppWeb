// ... (usingy bez zmian)

using AppWeb.Application.Cottage.Commands.EditCottage;
using AppWeb.Application.Cottage.Queries.GetAllCottages;
using AppWeb.Application.Cottage.Queries.GetForOneCottage;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppWeb.MVC.Controllers
{
    public class CottageController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CottageController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: Wyświetlanie listy
        [HttpGet]
        public async Task<IActionResult> IndexList()
        {
            var cottageAll = await _mediator.Send(new GetAllCottagesQuery());
            return View(cottageAll);
        }

        // GET: Formularz tworzenia
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // GET: Szczegóły domku
        [HttpGet]
        [Route("Cottage/{encodedName}/Details")] // Zmieniłem dla jasności
        public async Task<IActionResult> CottageMore(string encodedName)
        {
            var cottageDto = await _mediator.Send(new GetForOneCottageQuery(encodedName));
            if (cottageDto == null) return NotFound();
            return View(cottageDto);
        }

        // GET: FORMULARZ EDYCJI (To otwiera stronę)
        [HttpGet]
        [Route("Cottage/Edit/{encodedName}")] // ZMIANA: Teraz zadziała adres /Cottage/Edit/nazwa
        public async Task<IActionResult> Edit(string encodedName)
        {
            // Używamy GetForOneCottageQuery, które już masz!
            var cottageDto = await _mediator.Send(new GetForOneCottageQuery(encodedName));

            if (cottageDto == null) return NotFound();

            // Mapujemy DTO na komendę edycji
            var command = _mapper.Map<EditCottageCommand>(cottageDto);

            // Ważne: Jeśli plik nazywa się EditCottage.cshtml, musimy to podać:
            return View("EditCottage", command);
        }

        // POST: ZAPIS EDYCJI
        [HttpPost]
        [Route("Cottage/Edit/{encodedName}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string encodedName, EditCottageCommand command)
        {
            if (!ModelState.IsValid) return View("EditCottage", command);

            await _mediator.Send(command);

            // Po edycji tekstu idziemy do zarządzania zdjęciami
            return RedirectToAction(nameof(CreateImage), new { encodedName = encodedName });
        }

        // --- ZDJĘCIA ---

        [HttpGet]
        [Route("Cottage/EditImages/{encodedName}")]
        public async Task<IActionResult> CreateImage(string encodedName)
        {
            var cottageDto = await _mediator.Send(new GetForOneCottageQuery(encodedName));
            if (cottageDto == null) return NotFound();

            ViewBag.EncodedName = encodedName;
            return View(cottageDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateImage(string encodedName, List<IFormFile> ImageFiles)
        {
            // ... (logika zapisu zdjęć którą masz jest ok)
            // Na końcu:
            return RedirectToAction(nameof(CottageMore), new { encodedName = encodedName });
        }
    }
}