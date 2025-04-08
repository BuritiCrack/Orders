using Orders_Shared.Entities;

namespace Orders_Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountries();
            await CheckCategories();
        }

        private async Task CheckCategories()
        {
            if (!_context.Categories.Any())
            {
                await _context.Categories.AddAsync(new Category { Name = "Calzado" });
                await _context.Categories.AddAsync(new Category { Name = "Hogar" });
                await _context.Categories.AddAsync(new Category { Name = "Marroquineria" });
                await _context.Categories.AddAsync(new Category { Name = "Tecnologia" });
                await _context.Categories.AddAsync(new Category { Name = "Jueguetes" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountries()
        {
            if (!_context.Countries.Any())
            {
                await _context.Countries.AddAsync(new Country { Name = "Colombia" });
                await _context.Countries.AddAsync(new Country { Name = "Argentina" });
                await _context.Countries.AddAsync(new Country { Name = "Italia" });
                await _context.Countries.AddAsync(new Country { Name = "Grecia" });
                await _context.Countries.AddAsync(new Country { Name = "Perú" });

                await _context.SaveChangesAsync();
            }
        }
    }
}