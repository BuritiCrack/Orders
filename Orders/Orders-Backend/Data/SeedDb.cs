using Microsoft.EntityFrameworkCore;
using Orders_Backend.Helpers;
using Orders_Backend.UnitOfWork.Interfaces;
using Orders_Shared.Entities;
using Orders_Shared.Enums;

namespace Orders_Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly IFileStorage _fileStorage;

        public SeedDb(DataContext context, IUsersUnitOfWork usersUnitOfWork, IFileStorage fileStorage)
        {
            _context = context;
            _usersUnitOfWork = usersUnitOfWork;
            _fileStorage = fileStorage;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesFullAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
            await CheckRolesAsync();
            await CheckProductsAsync();
            await CheckUserAsync("1010", "Tomate", "Bedoya", "tommy@yopmail.com", "311 7779 8681",
                "Calle Itagui","Tomate.jpg", UserType.Admin);
            await CheckUserAsync("0002", "Ledys", "Bedoya", "ledys@yopmail.com", "322 311 4620",
                "Calle Luna Calle Sol", "LedysBedoya.jpg", UserType.User);
            await CheckUserAsync("0003", "Brad", "Pitt", "brad@yopmail.com", "322 311 4620",
                "Calle Luna Calle Sol", "Brad.jpg", UserType.User);
            await CheckUserAsync("0004", "Angelina", "Jolie", "angelina@yopmail.com", "322 311 4620",
                "Calle Luna Calle Sol", "Angelina.jpg", UserType.User);
            await CheckUserAsync("0005", "Bob", "Marley", "bob@yopmail.com", "322 311 4620",
                "Calle Luna Calle Sol", "bob.jpg", UserType.User);
            await CheckUserAsync("0006", "Celia", "Cruz", "celia@yopmail.com", "322 311 4620",
                "Calle Luna Calle Sol", "celia.jpg", UserType.Admin);
            await CheckUserAsync("0007", "Fredy", "Mercury", "fredy@yopmail.com", "322 311 4620",
                "Calle Luna Calle Sol", "fredy.jpg", UserType.User);
            await CheckUserAsync("0008", "Hector", "Lavoe", "hector@yopmail.com", "322 311 4620",
                "Calle Luna Calle Sol", "hector.jpg", UserType.User);
            await CheckUserAsync("0009", "Liv", "Taylor", "liv@yopmail.com", "322 311 4620",
                "Calle Luna Calle Sol", "liv.jpg", UserType.User);
            await CheckUserAsync("0010", "Otep", "Shamaya", "otep@yopmail.com", "322 311 4620",
                "Calle Luna Calle Sol", "otep.jpg", UserType.User);
            await CheckUserAsync("0011", "Ozzy", "Osbourne", "ozzy@yopmail.com", "322 311 4620",
                "Calle Luna Calle Sol", "ozzy.jpg", UserType.User);
            await CheckUserAsync("0012", "Selena", "Quintanilla", "selenba@yopmail.com", "322 311 4620",
                "Calle Luna Calle Sol", "selena.jpg", UserType.User);
        }

        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                await AddProductAsync("Adidas Barracuda", 270000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "adidas_barracuda.png" });
                await AddProductAsync("Adidas Superstar", 250000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "Adidas_superstar.png" });
                await AddProductAsync("Aguacate", 5000M, 500F, new List<string>() { "Comida" }, new List<string>() { "Aguacate1.png", "Aguacate2.png", "Aguacate3.png" });
                await AddProductAsync("AirPods", 1300000M, 12F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "airpos.png", "airpos2.png" });
                await AddProductAsync("Akai APC40 MKII", 2650000M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "Akai1.png", "Akai2.png", "Akai3.png" });
                await AddProductAsync("Apple Watch Ultra", 4500000M, 24F, new List<string>() { "Apple", "Tecnología" }, new List<string>() { "AppleWatchUltra1.png", "AppleWatchUltra2.png" });
                await AddProductAsync("Audifonos Bose", 870000M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "audifonos_bose.png" });
                await AddProductAsync("Bicicleta Ribble", 12000000M, 6F, new List<string>() { "Deportes" }, new List<string>() { "bicicleta_ribble.png" });
                await AddProductAsync("Camisa Cuadros", 56000M, 24F, new List<string>() { "Ropa" }, new List<string>() { "camisa_cuadros.png" });
                await AddProductAsync("Casco Bicicleta", 820000M, 12F, new List<string>() { "Deportes" }, new List<string>() { "casco_bicicleta.png", "casco.png" });
                await AddProductAsync("Gafas deportivas", 160000M, 24F, new List<string>() { "Deportes" }, new List<string>() { "Gafas1.png", "Gafas2.png", "Gafas3.png" });
                await AddProductAsync("Hamburguesa triple carne", 25500M, 240F, new List<string>() { "Comida" }, new List<string>() { "Hamburguesa1.png", "Hamburguesa2.png", "Hamburguesa3.png" });
                await AddProductAsync("iPad", 2300000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "ipad.png" });
                await AddProductAsync("iPhone 13", 5200000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "iphone13.png", "iphone13b.png", "iphone13c.png", "iphone13d.png" });
                await AddProductAsync("Johnnie Walker Blue Label 750ml", 1266700M, 18F, new List<string>() { "Licores" }, new List<string>() { "JohnnieWalker3.png", "JohnnieWalker2.png", "JohnnieWalker1.png" });
                await AddProductAsync("KOOY Disfraz inflable de gallo para montar", 150000M, 28F, new List<string>() { "Juguetes" }, new List<string>() { "KOOY1.png", "KOOY2.png", "KOOY3.png" });
                await AddProductAsync("Mac Book Pro", 12100000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "mac_book_pro.png" });
                await AddProductAsync("Mancuernas", 370000M, 12F, new List<string>() { "Deportes" }, new List<string>() { "mancuernas.png" });
                await AddProductAsync("Mascarilla Cara", 26000M, 100F, new List<string>() { "Belleza" }, new List<string>() { "mascarilla_cara.png" });
                await AddProductAsync("New Balance 530", 180000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "newbalance530.png" });
                await AddProductAsync("New Balance 565", 179000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "newbalance565.png" });
                await AddProductAsync("Nike Air", 233000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "nike_air.png" });
                await AddProductAsync("Nike Zoom", 249900M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "nike_zoom.png" });
                await AddProductAsync("Buso Adidas Mujer", 134000M, 12F, new List<string>() { "Ropa", "Deportes" }, new List<string>() { "buso_adidas.png" });
                await AddProductAsync("Suplemento Boots Original", 15600M, 12F, new List<string>() { "Nutrición" }, new List<string>() { "Boost_Original.png" });
                await AddProductAsync("Whey Protein", 252000M, 12F, new List<string>() { "Nutrición" }, new List<string>() { "whey_protein.png" });
                await AddProductAsync("Arnes Mascota", 25000M, 12F, new List<string>() { "Mascotas" }, new List<string>() { "arnes_mascota.png" });
                await AddProductAsync("Cama Mascota", 99000M, 12F, new List<string>() { "Mascotas" }, new List<string>() { "cama_mascota.png" });
                await AddProductAsync("Teclado Gamer", 67000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "teclado_gamer.png" });
                await AddProductAsync("Ring de Lujo 17", 1600000M, 33F, new List<string>() { "Autos" }, new List<string>() { "Ring1.png", "Ring2.png" });
                await AddProductAsync("Silla Gamer", 980000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "silla_gamer.png" });
                await AddProductAsync("Mouse Gamer", 132000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "mouse_gamer.png" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddProductAsync(string name, decimal price, float stock, List<string> categories, List<string> images)
        {
            Product prodcut = new()
            {
                Description = name,
                Name = name,
                Price = price,
                Stock = stock,
                ProductCategories = new List<ProductCategory>(),
                ProductImages = new List<ProductImage>()
            };

            foreach (var categoryName in categories)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
                if (category != null)
                {
                    prodcut.ProductCategories.Add(new ProductCategory { Category = category });
                }
            }

            foreach (string? image in images)
            {
                var filePath = $"{Environment.CurrentDirectory}\\Images\\products\\{image}";
                var fileBytes = File.ReadAllBytes(filePath);
                var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "products");
                prodcut.ProductImages.Add(new ProductImage { Image = imagePath });
            }

            _context.Products.Add(prodcut);
        }

        private async Task CheckCountriesFullAsync()
        {
            if (!_context.Countries.Any())
            {
                var countriesStatesCitiesSQLScript = File.ReadAllText("Data\\CountriesStatesCities.sql");
                await _context.Database.ExecuteSqlRawAsync(countriesStatesCitiesSQLScript);
            }
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName,
            string email, string phone, string address, string image, UserType userType)
        {
            var user = await _usersUnitOfWork.GetUserAsync(email);
            if (user == null)
            {
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Medellín");
                city ??= await _context.Cities.FirstOrDefaultAsync();

                var filePath = $"{Environment.CurrentDirectory}\\Images\\users\\{image}";
                var fileBytes = File.ReadAllBytes(filePath);
                var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "users");

                user = new User
                {
                    Document = document,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phone,
                    Address = address,
                    UserName = email,
                    City = city,
                    UserType = userType,
                    Photo = imagePath,
                };

                var result = await _usersUnitOfWork.AddUserAsync(user, "123456");
                if (result.Succeeded)
                {
                    await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());
                }
                var token = await _usersUnitOfWork.GenerateEmailConfirmationTokenAsync(user);
                await _usersUnitOfWork.ConfirmEmailAsync(user, token);
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