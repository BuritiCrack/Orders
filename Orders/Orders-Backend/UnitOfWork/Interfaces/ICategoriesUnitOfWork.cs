using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Responses;

namespace Orders_Backend.UnitOfWork.Interfaces
{
    public interface ICategoriesUnitOfWork
    {
        Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}