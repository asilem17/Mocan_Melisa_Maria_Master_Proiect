using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mocan_Melisa_MariaMVC.Data;
using Mocan_Melisa_MariaMVC.Models;
using Mocan_Melisa_MariaMVC.Services;

namespace Mocan_Melisa_MariaMVC.Controllers
{
    public class PetAdoptionController : Controller
    {
        private readonly IPetAdoptionService _service;
        private readonly PetsAdoptionContext _context;

        public PetAdoptionController(IPetAdoptionService service, PetsAdoptionContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pets = await _context.Pet.ToListAsync();
            ViewBag.Pets = new SelectList(pets, "Id", "PetName");
            return View(new PetAdoptionPredictionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(PetAdoptionPredictionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Pets = new SelectList(await _context.Pet.ToListAsync(), "Id", "PetName");
                return View(model);
            }

            var pet = await _context.Pet.FindAsync(model.PetId);
            if (pet == null)
            {
                ModelState.AddModelError("", "Pet not found.");
                ViewBag.Pets = new SelectList(await _context.Pet.ToListAsync(), "Id", "PetName");
                return View(model);
            }

            model.PetName = pet.PetName;
            model.AdoptionProbability = Math.Max(0f, Math.Min(await _service.PredictAdoptionProbabilityAsync(model.PetId), 1f));

            ViewBag.Pets = new SelectList(await _context.Pet.ToListAsync(), "Id", "PetName", model.PetId);

            return View(model);
        }

    }
}
