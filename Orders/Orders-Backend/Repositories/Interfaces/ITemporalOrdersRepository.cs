using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Responses;

namespace Orders_Backend.Repositories.Interfaces
{
    public interface ITemporalOrdersRepository
    {
        Task<ActionResponse<TemporalOrderDTO>> AddFullAsync(string email, TemporalOrderDTO temporalOrderDTO);

        Task<ActionResponse<IEnumerable<TemporalOrder>>> GetAsync(string email);

        Task<ActionResponse<int>> GetCountAsync(string email);
    }
}