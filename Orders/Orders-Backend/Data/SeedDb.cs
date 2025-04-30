using Orders_Backend.UnitOfWork.Interfaces;
using Orders_Shared.Entities;
using Orders_Shared.Enums;

namespace Orders_Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsersUnitOfWork _usersUnitOfWork;

        public SeedDb(DataContext context, IUsersUnitOfWork usersUnitOfWork)
        {
            _context = context;
            _usersUnitOfWork = usersUnitOfWork;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Tomate", "Bedoya", "tommy@mimail.com", "311 7779 8681",
                "Calle Itagui", UserType.Admin);
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName,
            string email, string phone, string address, UserType userType)
        {
            var user = await _usersUnitOfWork.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    Document = document,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phone,
                    Address = address,
                    UserName = email,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType
                };

                var result = await _usersUnitOfWork.AddUserAsync(user, "123456");
                if (result.Succeeded)
                {
                    await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());
                }
            }
            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.User.ToString());
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
                             new() {Name = "San Luis"},
                             new() {Name = "San Pedro de los Milagros"},
                             new() {Name = "San Vicente"},
                             new() {Name = "Santa Rosa de Osos"},
                             new() {Name = "Sabaneta"},
                             new() {Name = "Sonsón"},
                             new() {Name = "Sopetrán"},
                             new() {Name = "Turbo"},
                             new() {Name = "Valparaíso"},
                             new() {Name = "Yarumal"},
                             new() {Name = "Yondó"},
                             new() {Name = "Zaragoza"},
                             new() {Name = "Carmen de Viboral"},
                             new() {Name = "Cisneros"},
                             new() {Name = "Cocorná"},
                             new() {Name = "Concepción"},
                             new() {Name = "Don Matías"},
                             new() {Name = "El Bagre"},
                             new() {Name = "El Carmen de Viboral"},
                             new() {Name = "El Peñol"},
                             new() {Name = "El Retiro"},
                             new() {Name = "El Santuario"},
                             new() {Name = "Entrerríos"},
                             new() {Name = "Envigado"}
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
                await _context.Countries.AddAsync(new Country
                {
                    Name = "España",
                    States =
                [
                    new()
                        {
                            Name = "Andalucia",
                            Cities =
                            [
                                new() {Name = "Sevilla"},
                                new() {Name = "Cádiz"},
                                new() {Name = "Málaga"},
                                new() {Name = "Granada"},
                                new() {Name = "Jaén"},
                                new() {Name = "Córdoba"}
                            ]
                        },
                        new()
                        {
                            Name = "Cataluña",
                            Cities =
                            [
                                new() {Name = "Barcelona"},
                                new() {Name = "Girona"},
                                new() {Name = "Lérida"},
                                new() {Name = "Tarragona"}
                            ]
                        }
                ]
                });

                await _context.Countries.AddAsync(new Country
                {
                    Name = "Francia",
                    States =
                [
                    new()
                     {
                         Name = "Île-de-France",
                         Cities =
                         [
                             new() {Name = "París"},
                             new() {Name = "Versalles"},
                             new() {Name = "Saint-Denis"},
                             new() {Name = "Nanterre"},
                             new() {Name = "Créteil"},
                             new() {Name = "Bobigny"}
                         ]
                     },
                     new()
                     {
                         Name = "Provenza-Alpes-Costa Azul",
                         Cities =
                         [
                             new() {Name = "Marsella"},
                             new() {Name = "Niza"},
                             new() {Name = "Aix-en-Provence"},
                             new() {Name = "Avignon"},
                             new() {Name = "Toulon"}
                         ]
                     }

                ]
                });

                await _context.Countries.AddAsync(new Country { Name = "Chile" });
                await _context.Countries.AddAsync(new Country { Name = "Uruguay" });
                await _context.Countries.AddAsync(new Country { Name = "Paraguay" });
                await _context.Countries.AddAsync(new Country { Name = "Bolivia" });
                await _context.Countries.AddAsync(new Country { Name = "Ecuador" });
                await _context.Countries.AddAsync(new Country { Name = "Argentina" });
                await _context.Countries.AddAsync(new Country { Name = "Grecia" });
                await _context.Countries.AddAsync(new Country { Name = "Perú" });

                await _context.SaveChangesAsync();
            }
        }
    }
}