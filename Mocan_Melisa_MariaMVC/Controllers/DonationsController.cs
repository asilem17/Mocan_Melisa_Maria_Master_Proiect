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
    public class DonationsController : Controller
    {
        private readonly PetsAdoptionContext _context;

        public DonationsController(PetsAdoptionContext context)
        {
            _context = context;
        }

        // GET: Donations
        public async Task<IActionResult> Index()
        {
            var petsAdoptionContext = _context.Donation.Include(d => d.Pet).Include(d => d.User);
            return View(await petsAdoptionContext.ToListAsync());
        }

        // GET: Donations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donation
                .Include(d => d.Pet)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // GET: Donations/Create
        public IActionResult Create()
        {
            ViewData["PetId"] = new SelectList(_context.Set<Pet>(), "Id", "PetName");
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Email");
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,PetId,Amount,Type,Date")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetId"] = new SelectList(_context.Set<Pet>(), "Id", "PetName", donation.PetId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Email", donation.UserId);
            return View(donation);
        }

        // GET: Donations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donation.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            ViewData["PetId"] = new SelectList(_context.Set<Pet>(), "Id", "PetName", donation.PetId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Email", donation.UserId);
            return View(donation);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,PetId,Amount,Type,Date")] Donation donation)
        {
            if (id != donation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationExists(donation.Id))
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
            ViewData["PetId"] = new SelectList(_context.Set<Pet>(), "Id", "PetName", donation.PetId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Email", donation.UserId);
            return View(donation);
        }

        // GET: Donations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donation
                .Include(d => d.Pet)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _context.Donation.FindAsync(id);
            if (donation != null)
            {
                _context.Donation.Remove(donation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationExists(int id)
        {
            return _context.Donation.Any(e => e.Id == id);
        }
    }
}
