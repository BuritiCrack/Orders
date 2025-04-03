using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders_Backend.Data;
using Orders_Shared.Entities;

namespace Orders_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
        }

        // Metodo para crear un nuevo país
        [HttpPost]
        public async Task<IActionResult> PostAsync(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return Ok(country);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var countries = await _context.Countries.ToListAsync();
            return Ok(countries);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var conuntry = await _context.Countries.FindAsync(id);
            if (conuntry == null)
            {
                return NotFound();
            }
            return Ok(conuntry);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Country country)
        {
            _context.Countries.Update(country);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            var conuntry = await _context.Countries.FindAsync(id);
            if (conuntry == null)
            {
                return NotFound();
            }
            _context.Countries.Remove(conuntry);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}