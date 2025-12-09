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
    public class AdoptionRequestsController : Controller
    {
        private readonly PetsAdoptionContext _context;

        public AdoptionRequestsController(PetsAdoptionContext context)
        {
            _context = context;
        }

        // GET: AdoptionRequests
        public async Task<IActionResult> Index()
        {
            var petsAdoptionContext = _context.AdoptionRequest.Include(a => a.Pet).Include(a => a.User);
            return View(await petsAdoptionContext.ToListAsync());
        }

        // GET: AdoptionRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionRequest = await _context.AdoptionRequest
                .Include(a => a.Pet)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoptionRequest == null)
            {
                return NotFound();
            }

            return View(adoptionRequest);
        }

        // GET: AdoptionRequests/Create
        public IActionResult Create()
        {
            var pets = _context.Pet
            .Select(p => new
            {
                Id = p.Id,
                Name = p.PetName
            })
            .ToList();


            var petList = new List<object>
            {
                new { Id = (int?)null, Name = "Any pet available" }
            };
            petList.AddRange(pets.Cast<object>());

            ViewData["PetId"] = new SelectList(petList, "Id", "Name");

            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Email");
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

        // POST: AdoptionRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DesiredPetType,MinAgeMonths,MaxAgeMonths,DesiredSize,UserId,PetId,RequestDate")] AdoptionRequest adoptionRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adoptionRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetType"] = new SelectList(
            _context.Pet
                .Select(p => p.PetType)
                .Distinct()
                .ToList()
                .Select(pt => new { Value = pt, Text = pt }),
                    "Value",
                    "Text",
                    adoptionRequest.DesiredPetType
                );

            ViewData["PetId"] = new SelectList(
                _context.Pet.ToList(),
                "Id",
                "PetName",
                adoptionRequest.PetId
                );


            ViewData["UserId"] = new SelectList(
                _context.User.ToList(),
                "Id",
                "Email",
                adoptionRequest.UserId
            );
            ViewData["Sizes"] = new SelectList(new[]
            {
                "Small",
                "Medium",
                "Big"
            }, adoptionRequest.DesiredSize);
            

            return View(adoptionRequest);
        }

        // GET: AdoptionRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionRequest = await _context.AdoptionRequest.FindAsync(id);
            if (adoptionRequest == null)
            {
                return NotFound();
            }
            var pets = _context.Pet
            .Select(p => new
            {
                Id = p.Id,
                Name = p.PetName
            })
            .ToList();


            var petList = new List<object>
            {
                new { Id = (int?)null, Name = "Any pet available" }
            };
            petList.AddRange(pets.Cast<object>());

            ViewData["PetId"] = new SelectList(petList, "Id", "Name");

            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Email", adoptionRequest.UserId);
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

            return View(adoptionRequest);
        }

        // POST: AdoptionRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DesiredPetType,MinAgeMonths,MaxAgeMonths,DesiredSize,UserId,PetId,RequestDate")] AdoptionRequest adoptionRequest)
        {
            if (id != adoptionRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adoptionRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdoptionRequestExists(adoptionRequest.Id))
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
            ViewData["PetId"] = new SelectList(
            _context.Pet.ToList(),
            "Id",
            "PetName",
            adoptionRequest.PetId
            );
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Email", adoptionRequest.UserId);
            ViewData["PetType"] = new SelectList(
            _context.Pet
                .Select(p => p.PetType)
                .Distinct()
                .ToList()
                .Select(pt => new { Value = pt, Text = pt }),
                    "Value",
                    "Text",
                    adoptionRequest.DesiredPetType
                );
            ViewData["Sizes"] = new SelectList(new[]
            {
                "Small",
                "Medium",
                "Big"
            }, adoptionRequest.DesiredSize);

            return View(adoptionRequest);
        }

        // GET: AdoptionRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionRequest = await _context.AdoptionRequest
                .Include(a => a.Pet)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoptionRequest == null)
            {
                return NotFound();
            }

            return View(adoptionRequest);
        }

        // POST: AdoptionRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adoptionRequest = await _context.AdoptionRequest.FindAsync(id);
            if (adoptionRequest != null)
            {
                _context.AdoptionRequest.Remove(adoptionRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionRequestExists(int id)
        {
            return _context.AdoptionRequest.Any(e => e.Id == id);
        }
    }
}
