using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Orders_Backend.Data;
using Orders_Backend.Repositories.Implementations;
using Orders_Backend.Repositories.Interfaces;
using Orders_Backend.UnitOfWork.Implementations;
using Orders_Backend.UnitOfWork.Interfaces;
using System.Text.Json.Serialization;

namespace Orders_Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services
                .AddControllers()
                .AddJsonOptions(c => c.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Orders API",
                    Version = "v1"
                });
            });

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddTransient<SeedDb>();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));

            builder.Services.AddScoped(typeof(ICountriesRepository), typeof(CountriesRepository));
            builder.Services.AddScoped(typeof(ICountriesUnitOfWork), typeof(CountriesUnitOfWork));

            var app = builder.Build();

            //Inyecyamos el seedDB manualmente ya que el program no se deja inyectar
            SeedData(app);

            void SeedData(WebApplication app)
            {
                var scopedFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

                using (var scoped = scopedFactory!.CreateScope())
                {
                    var service = scoped.ServiceProvider.GetService<SeedDb>();
                    service!.SeedAsync().Wait();
                }
            }

            app.UseCors(c => c
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}