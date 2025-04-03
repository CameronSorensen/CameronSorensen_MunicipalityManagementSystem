using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MunicipalityManagementSystem.Data;
using MunicipalityManagementSystem.Models;
using System.Threading.Tasks;

namespace MunicipalityManagementSystem.Controllers
{
    public class CitizenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CitizenController> _logger;

        public CitizenController(ApplicationDbContext context, ILogger<CitizenController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Citizen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Citizens.ToListAsync());
        }

        // GET: Citizen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizens
                .FirstOrDefaultAsync(m => m.CitizenID == id);
            if (citizen == null)
            {
                return NotFound();
            }

            return View(citizen);
        }

        // GET: Citizen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Citizen/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitizenID,FullName,Address,PhoneNumber,Email,DateOfBirth")] Citizen citizen)
        {
            if (ModelState.IsValid)
            {
                citizen.RegistrationDate = DateTime.Now;
                _context.Add(citizen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(citizen);
        }

        // GET: Citizen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizens.FindAsync(id);
            if (citizen == null)
            {
                return NotFound();
            }
            return View(citizen);
        }

        // POST: Citizen/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CitizenID,FullName,Address,PhoneNumber,Email,DateOfBirth,RegistrationDate")] Citizen citizen)
        {
            if (id != citizen.CitizenID)
            {
                _logger.LogWarning("Citizen ID mismatch: {CitizenID}", citizen.CitizenID);
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citizen);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Citizen updated successfully: {CitizenID}", citizen.CitizenID);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitizenExists(citizen.CitizenID))
                    {
                        _logger.LogWarning("Citizen not found: {CitizenID}", citizen.CitizenID);
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError("Concurrency exception: {CitizenID}", citizen.CitizenID);
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            _logger.LogWarning("Model state is invalid for Citizen ID: {CitizenID}", citizen.CitizenID);
            return View(citizen);
        }

        // GET: Citizen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizens
                .FirstOrDefaultAsync(m => m.CitizenID == id);
            if (citizen == null)
            {
                return NotFound();
            }

            return View(citizen);
        }

        // POST: Citizen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var citizen = await _context.Citizens.FindAsync(id);
            _context.Citizens.Remove(citizen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitizenExists(int id)
        {
            return _context.Citizens.Any(e => e.CitizenID == id);
        }
    }
}
