using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mocan_Melisa_MariaMVC.Data;
using Mocan_Melisa_MariaMVC.Models;

namespace Mocan_Melisa_MariaMVC.Controllers
{
    public class PetsController : Controller
    {
        private readonly PetsAdoptionContext _context;

        public PetsController(PetsAdoptionContext context)
        {
            _context = context;
        }

        // GET: Pets
        public async Task<IActionResult> Index()
        {
            var petsAdoptionContext = _context.Pet.Include(p => p.BreedNavigation);
            return View(await petsAdoptionContext.ToListAsync());
        }

        // GET: Pets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pet
                .Include(p => p.BreedNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Pets/Create
        public IActionResult Create()
        {
            ViewData["BreedId"] = new SelectList(_context.Breed, "Id", "BreedName");
            var healthOptions = new List<SelectListItem>
            {
            new SelectListItem { Value = "0-Healthy", Text = "0 - Healthy" },
            new SelectListItem { Value = "1-Medical condition", Text = "1 - Medical condition" }
            };
            ViewData["HealthConditionList"] = new SelectList(healthOptions, "Value", "Text");
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PetName,PetType,AgeMonths,Size,Vaccinated,HealthCondition,TimeInShelterDays,BreedId")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BreedId"] = new SelectList(_context.Breed, "Id", "BreedName", pet.BreedId);

            var healthOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "0-Healthy", Text = "0 - Healthy" },
                new SelectListItem { Value = "1-Medical condition", Text = "1 - Medical condition" }
            };
            ViewData["HealthConditionList"] = new SelectList(healthOptions, "Value", "Text", pet.HealthCondition);
            return View(pet);
        }

        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pet.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            ViewData["BreedId"] = new SelectList(_context.Breed, "Id", "BreedName", pet.BreedId);
            var healthOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "0-Healthy", Text = "0 - Healthy" },
                new SelectListItem { Value = "1-Medical condition", Text = "1 - Medical condition" }
            };

            ViewData["HealthConditionList"] = new SelectList(healthOptions, "Value", "Text");
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PetName,PetType,AgeMonths,Size,Vaccinated,HealthCondition,TimeInShelterDays,BreedId")] Pet pet)
        {
            if (id != pet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BreedId"] = new SelectList(_context.Breed, "Id", "BreedName", pet.BreedId);

            var healthOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "0-Healthy", Text = "0 - Healthy" },
                new SelectListItem { Value = "1-Medical condition", Text = "1 - Medical condition" }
            };
            ViewData["HealthConditionList"] = new SelectList(healthOptions, "Value", "Text", pet.HealthCondition);
            return View(pet);
        }

        // GET: Pets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pet
                .Include(p => p.BreedNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _context.Pet.FindAsync(id);
            if (pet != null)
            {
                _context.Pet.Remove(pet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(int id)
        {
            return _context.Pet.Any(e => e.Id == id);
        }
    }
}
