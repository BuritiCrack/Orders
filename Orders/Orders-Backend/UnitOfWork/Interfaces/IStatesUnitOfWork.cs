using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Responses;

namespace Orders_Backend.UnitOfWork.Interfaces
{
    public interface IStatesUnitOfWork
    {
        Task<ActionResponse<State>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<State>>> GetAsync();

        Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<IEnumerable<State>> GetComboAsync(int countryId);
    }
}