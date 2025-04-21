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
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                await _context.Categories.AddAsync(new Category { Name = "Calzado" });
                await _context.Categories.AddAsync(new Category { Name = "Hogar" });
                await _context.Categories.AddAsync(new Category { Name = "Marroquineria" });
                await _context.Categories.AddAsync(new Category { Name = "Tecnologia" });
                await _context.Categories.AddAsync(new Category { Name = "Jueguetes" });
                await _context.Categories.AddAsync(new Category { Name = "Ropa" });
                await _context.Categories.AddAsync(new Category { Name = "Deportes" });
                await _context.Categories.AddAsync(new Category { Name = "Belleza" });
                await _context.Categories.AddAsync(new Category { Name = "Salud" });
                await _context.Categories.AddAsync(new Category { Name = "Alimentos" });
                await _context.Categories.AddAsync(new Category { Name = "Mascotas" });
                await _context.Categories.AddAsync(new Category { Name = "Libros" });
                await _context.Categories.AddAsync(new Category { Name = "Accesorios" });
                await _context.Categories.AddAsync(new Category { Name = "Electrodomesticos" });
                await _context.Categories.AddAsync(new Category { Name = "Ferreteria" });
                await _context.Categories.AddAsync(new Category { Name = "Construccion" });
                await _context.Categories.AddAsync(new Category { Name = "Jardineria" });
                await _context.Categories.AddAsync(new Category { Name = "Bebes" });
                await _context.Categories.AddAsync(new Category { Name = "Papeleria" });
                await _context.Categories.AddAsync(new Category { Name = "Cuidado Personal" });
                await _context.Categories.AddAsync(new Category { Name = "Cuidado del Hogar" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                await _context.Countries.AddAsync(new Country
                {
                    Name = "Colombia",
                    States =
                [
                    new()
                    {
                        Name = "Antioquia",
                        Cities =
                        [
                            new() {Name = "Medellín"},
                            new() {Name = "San Rafael"},
                            new() {Name = "San Roque"},
                            new() {Name = "San Carlos"},
                            new() {Name = "Granada"},
                            new() {Name = "San Luis"}
                        ]
                    },
                    new()
                    {
                        Name = "Vaupés",
                        Cities =
                        [
                            new() {Name = "Mitú"},
                            new() {Name = "Carurú"},
                            new() {Name = "Taraira"}
                        ]
                    },
                    new()
                    {
                        Name = "Valle del Cauca",
                        Cities =
                        [
                            new() {Name = "Calí"},
                            new() {Name = "Tulua"},
                            new() {Name = "Bugalagrande"},
                            new() {Name = "Cartago"},
                            new() {Name = "Zarzal"},
                            new() {Name = "La Unión"},
                        ]
                    }
                ]
                });

                await _context.Countries.AddAsync(new Country
                {
                    Name = "Italia",
                    States =
                [
                    new()
                    {
                        Name = "Lombardia",
                        Cities =
                        [
                            new() {Name = "Milán"},
                            new() {Name = "Bergamo"},
                            new() {Name = "Como"},
                            new() {Name = "Varese"},
                            new() {Name = "Brescia"},
                            new() {Name = "Cremonta"}
                        ]
                    },
                    new()
                    {
                        Name = "Sicilia",
                        Cities =
                        [
                            new() {Name = "Palermo"},
                            new() {Name = "Catania"},
                            new() {Name = "Ragusa"},
                            new() {Name = "Enna"},
                            new() {Name = "Messina"},
                            new() {Name = "Trapani"}
                        ]
                    }

                ]
                });

                await _context.Countries.AddAsync(new Country { Name = "Argentina" });
                await _context.Countries.AddAsync(new Country { Name = "Grecia" });
                await _context.Countries.AddAsync(new Country { Name = "Perú" });

                await _context.SaveChangesAsync();
            }
        }
    }
}