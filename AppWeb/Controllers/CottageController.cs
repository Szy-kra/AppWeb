using AppWeb.Application.Cottage.Commands.AddCottageImages;
using AppWeb.Application.Cottage.Commands.CreateCottage;
using AppWeb.Application.Cottage.Commands.DeleteCottage;
using AppWeb.Application.Cottage.Commands.EditCottage;
using AppWeb.Application.Cottage.Queries.GetAllCottages;
using AppWeb.Application.Cottage.Queries.GetContact;
using AppWeb.Application.Cottage.Queries.GetCottageImages;
using AppWeb.Application.Cottage.Queries.GetForOneCottage;
using AppWeb.Application.Cottage.Queries.GetUserCottages;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        // Akcja obsługuje teraz oba adresy używane w aplikacji
        [HttpGet]
        [Route("Cottage/IndexList")]
        [Route("Cottage/Index")]
        public async Task<IActionResult> IndexList()
        {
            var cottageAll = await _mediator.Send(new GetAllCottagesQuery());
            return View(cottageAll);
        }

        // Lista domków przypisanych do zalogowanego użytkownika
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UserCottages()
        {
            var userCottages = await _mediator.Send(new GetUserCottagesQuery());
            return View(userCottages);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCottageCommand command)
        {
            if (!ModelState.IsValid) return View(command);
            var newEncodedName = await _mediator.Send(command);
            return RedirectToAction(nameof(CreateImage), new { encodedName = newEncodedName });
        }

        [HttpGet]
        [Authorize]
        [Route("Cottage/EditImages/{encodedName}")]
        public async Task<IActionResult> CreateImage(string encodedName)
        {
            var cottageDto = await _mediator.Send(new GetForOneCottageQuery(encodedName));
            if (cottageDto == null) return NotFound();
            ViewBag.EncodedName = encodedName;
            return View(cottageDto);
        }

        [HttpPost]
        [Authorize]
        [Route("Cottage/EditImages/{encodedName}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateImage(string encodedName, List<IFormFile> ImageFiles)
        {
            if (string.IsNullOrEmpty(encodedName)) return BadRequest();

            if (ImageFiles != null && ImageFiles.Any())
            {
                var imagePaths = ImageFiles
                    .Where(f => f != null)
                    .Select(f => $"/DataImage/{f.FileName}")
                    .ToList();

                await _mediator.Send(new AddCottageImagesCommand
                {
                    EncodedName = encodedName,
                    ImagePaths = imagePaths
                });
            }

            return RedirectToAction(nameof(CottageMore), new { encodedName = encodedName });
        }

        [HttpGet]
        [Route("Cottage/{encodedName}/Details")]
        public async Task<IActionResult> CottageMore(string encodedName)
        {
            var cottageDto = await _mediator.Send(new GetForOneCottageQuery(encodedName));
            if (cottageDto == null) return NotFound();

            var imageUrls = await _mediator.Send(new GetCottageImagesQuery(encodedName));
            ViewBag.ImageUrls = imageUrls;

            // Sekcja Kontaktu - POPRAWIONA
            try
            {
                // Przesyłamy encodedName zamiast null, aby Handler wiedział o który domek chodzi
                var contactInfo = await _mediator.Send(new GetContactQuery(encodedName));

                // Przypisujemy wynik do ViewData, aby widok mógł go odebrać
                ViewData["CurrentUser"] = contactInfo;
            }
            catch (Exception)
            {
                // W razie błędu przesyłamy null
                ViewData["CurrentUser"] = null;
            }

            return View(cottageDto);
        }

        [HttpGet]
        [Authorize]
        [Route("Cottage/Edit/{encodedName}")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var cottageDto = await _mediator.Send(new GetForOneCottageQuery(encodedName));
            if (cottageDto == null) return NotFound();

            var command = _mapper.Map<EditCottageCommand>(cottageDto);

            return View("EditCottage", command);
        }

        [HttpPost]
        [Authorize]
        [Route("Cottage/Edit/{encodedName}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string encodedName, EditCottageCommand command)
        {
            if (!ModelState.IsValid) return View("EditCottage", command);
            var resultName = await _mediator.Send(command);
            return RedirectToAction(nameof(CreateImage), new { encodedName = resultName });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string encodedName)
        {
            await _mediator.Send(new DeleteCottageCommand(encodedName));
            return RedirectToAction(nameof(UserCottages));
        }
    }
}