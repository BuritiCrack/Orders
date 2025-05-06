using Microsoft.EntityFrameworkCore;
using Orders_Backend.Data;
using Orders_Backend.Helpers;
using Orders_Backend.Repositories.Interfaces;
using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Responses;

namespace Orders_Backend.Repositories.Implementations
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly DataContext _context;

        public CountriesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination)
        {
            var query = _context.Countries
                .Include(c => c.States)
                .AsQueryable();

            if (!string.IsNullOrEmpty(pagination.Filter))
            {
                query = query.Where(c => c.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Country>>()
            {
                WasSuccess = true,
                Result = await query
                        .OrderBy(c => c.Name)
                        .Paginate(pagination)
                        .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var query = _context.Countries.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                query = query.Where(c => c.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }
            double count = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>()
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public override async Task<ActionResponse<Country>> GetAsync(int id)
        {
            var country = await _context.Countries
                .Include(c => c.States!)
                .ThenInclude(c => c.Cities)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (country == null)
            {
                return new ActionResponse<Country>()
                {
                    WasSuccess = false,
                    Message = "Este país no existe"
                };
            }

            return new ActionResponse<Country>()
            {
                WasSuccess = true,
                Result = country
            };
        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
        {
            var countries = await _context.Countries
                .Include(c => c.States)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Country>>()
            {
                WasSuccess = true,
                Result = countries
            };
        }

        public async Task<IEnumerable<Country>> GetComboAsync()
        {
            return await _context.Countries
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}