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
        private readonly IWebHostEnvironment _env;

        public PetsController(PetsAdoptionContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
            new SelectListItem { Value = "Healthy", Text = "Healthy" },
            new SelectListItem { Value = "Medical condition", Text = "Medical condition" }
            };
            ViewData["HealthConditionList"] = new SelectList(healthOptions, "Value", "Text");
            ViewData["PetType"] = new SelectList(_context.Pet
            .Select(p => p.PetType)
            .Distinct()
            .ToList()
            .Select(pt => new { Value = pt, Text = pt }),
                "Value",
                "Text"
            );
            ViewData["Sizes"] = new SelectList(new[]
            {
                 "Small",
                 "Medium",
                 "Big"
             });
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PetName,PetType,AgeMonths,Size,Vaccinated,HealthCondition,TimeInShelterDays,BreedId")] Pet pet, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pet);
                await _context.SaveChangesAsync();
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    SavePetImage(ImageFile, pet.Id);
                }
                return RedirectToAction(nameof(Index));

            }

            ViewData["BreedId"] = new SelectList(_context.Breed, "Id", "BreedName", pet.BreedId);

            var healthOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "Healthy", Text = "Healthy" },
                new SelectListItem { Value = "Medical condition", Text = "Medical condition" }
            };
            ViewData["HealthConditionList"] = new SelectList(healthOptions, "Value", "Text", pet.HealthCondition);

            ViewData["PetType"] = new SelectList(
            _context.Pet
                .Select(p => p.PetType)
                .Distinct()
                .ToList()
                .Select(pt => new { Value = pt, Text = pt }),
                    "Value",
                    "Text",
                    pet.PetType
                );
            ViewData["Sizes"] = new SelectList(new[]
            {
                "Small",
                "Medium",
                "Big"
            }, pet.Size);
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
                new SelectListItem { Value = "Healthy", Text = "Healthy" },
                new SelectListItem { Value = "Medical condition", Text = "Medical condition" }
            };

            ViewData["HealthConditionList"] = new SelectList(healthOptions, "Value", "Text");
            ViewData["PetType"] = new SelectList(_context.Pet
            .Select(p => p.PetType)
            .Distinct()
            .ToList()
            .Select(pt => new { Value = pt, Text = pt }),
                "Value",
                "Text"
            );
            ViewData["Sizes"] = new SelectList(new[]
            {
                 "Small",
                 "Medium",
                 "Big"
             });
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PetName,PetType,AgeMonths,Size,Vaccinated,HealthCondition,TimeInShelterDays,BreedId")] Pet pet, IFormFile ImageFile)
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
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        SavePetImage(ImageFile, pet.Id);
                    }
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
                new SelectListItem { Value = "Healthy", Text = "Healthy" },
                new SelectListItem { Value = "Medical condition", Text = "Medical condition" }
            };
            ViewData["HealthConditionList"] = new SelectList(healthOptions, "Value", "Text", pet.HealthCondition);
           
            ViewData["Sizes"] = new SelectList(new[]
            {
                "Small",
                "Medium",
                "Big"
            }, pet.Size);
            ViewData["PetType"] = new SelectList(
            _context.Pet
                .Select(p => p.PetType)
                .Distinct()
                .ToList()
                .Select(pt => new { Value = pt, Text = pt }),
                    "Value",
                    "Text",
                    pet.PetType
                );
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
        private void SavePetImage(IFormFile file, int petId)
        {
            // allowed extensions
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowed.Contains(ext)) ext = ".jpg"; // fallback

            var imagesFolder = Path.Combine(_env.WebRootPath, "images", "pets");
            if (!Directory.Exists(imagesFolder)) Directory.CreateDirectory(imagesFolder);

            var filePath = Path.Combine(imagesFolder, $"{petId}{ext}");

            // delete old files with other extensions for same id (optional cleanup)
            var existing = Directory.GetFiles(imagesFolder, $"{petId}.*");
            foreach (var f in existing)
            {
                try { System.IO.File.Delete(f); } catch { /* ignore */ }
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }


        private bool PetExists(int id)
        {
            return _context.Pet.Any(e => e.Id == id);
        }
    }
}
