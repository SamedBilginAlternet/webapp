using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PackageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Package
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackages()
        {
            return await _context.Packages.ToListAsync();
        }

        // GET: api/Package/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(Guid id)
        {
            var package = await _context.Packages.FindAsync(id);

            if (package == null)
            {
                return NotFound();
            }

            // Example of handling null values
            bool isNormalPackageAvailable = package.NormalPackage ?? false;
            bool isPremiumPackageAvailable = package.PremiumPackage ?? false;
            bool isEconomicPackageAvailable = package.EconomicPackage ?? false;

            // Optionally, you can return the package with more details
            return package;
        }

        // GET: api/Package/Firm/5
        [HttpGet("Firm/{firmaId}")]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackagesByFirma(Guid firmaId)
        {
            var packages = await _context.Packages
                .Where(p => p.FirmaId == firmaId)
                .ToListAsync();

            if (packages == null || !packages.Any())
            {
                return NotFound();
            }

            return packages;
        }

        // PUT: api/Package/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackage(Guid id, Package package)
        {
            if (id != package.PackageId)
            {
                return BadRequest();
            }

            // Ensure the entity state is modified
            _context.Entry(package).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Package
        [HttpPost]
        public async Task<ActionResult<Package>> PostPackage(Package package)
        {
            // Validate the incoming package
            if (package == null || package.FirmaId == Guid.Empty || package.PackageId == Guid.Empty)
            {
                return BadRequest("Invalid package data.");
            }

            _context.Packages.Add(package);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPackage), new { id = package.PackageId }, package);
        }

        // DELETE: api/Package/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(Guid id)
        {
            var package = await _context.Packages.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }

            _context.Packages.Remove(package);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageExists(Guid id)
        {
            return _context.Packages.Any(e => e.PackageId == id);
        }
    }
}
