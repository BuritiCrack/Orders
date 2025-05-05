using Microsoft.EntityFrameworkCore;
using Orders_Backend.Data;
using Orders_Backend.Helpers;
using Orders_Backend.Repositories.Interfaces;
using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Responses;

namespace Orders_Backend.Repositories.Implementations
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        private readonly DataContext _context;

        public CategoriesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination)
        {
            var query = _context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(pagination.Filter))
            {
                query = query.Where(c => c.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }
            return new ActionResponse<IEnumerable<Category>>
            {
                WasSuccess = true,
                Result = await query
                    .OrderBy(c => c.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public async Task<IEnumerable<Category>> GetComboAsync()
        {
            return await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var query = _context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(pagination.Filter))
            {
                query = query.Where(c => c.Name.ToLower().Contains(pagination.Filter.ToLower()));
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
