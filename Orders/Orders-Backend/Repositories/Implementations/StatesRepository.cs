using Microsoft.EntityFrameworkCore;
using Orders_Backend.Data;
using Orders_Backend.Helpers;
using Orders_Backend.Repositories.Interfaces;
using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Responses;

namespace Orders_Backend.Repositories.Implementations
{
    public class StatesRepository : GenericRepository<State>, IStatesRepository
    {
        private readonly DataContext _context;

        public StatesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<State>> GetAsync(int id)
        {
            var state = await _context.States
                .Include(s => s.Cities)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (state == null)
            {
                return new ActionResponse<State>
                {
                    WasSuccess = false,
                    Message = "Este estado no existe"
                };
            }

            return new ActionResponse<State>
            {
                WasSuccess = true,
                Result = state
            };
        }

        public override async Task<ActionResponse<IEnumerable<State>>> GetAsync()
        {
            var states = await _context.States
                .OrderBy(s => s.Name)
                .Include(s => s.Cities)
                .ToListAsync();
            return new ActionResponse<IEnumerable<State>>
            {
                WasSuccess = true,
                Result = states
            };
        }

        public override async Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination)
        {
            var query = _context.States
                .Include(s => s.Cities)
                .Where(s => s.Country!.Id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrEmpty(pagination.Filter))
            {
                query = query.Where(s => s.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }
                return new ActionResponse<IEnumerable<State>>()
            {
                WasSuccess = true,
                Result = await query
                    .OrderBy(s => s.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public async Task<IEnumerable<State>> GetComboAsync(int countryId)
        {
            return await _context.States
                .Where(s => s.CountryId == countryId)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var query = _context.States
                .Where(s => s.Country!.Id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrEmpty(pagination.Filter))
            {
                query = query.Where(s => s.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }
    }
}