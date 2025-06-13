using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Responses;
using System.Threading.Tasks;

namespace Orders_Backend.Repositories.Interfaces
{
    public interface IOrdersRepository
    {
        Task<ActionResponse<IEnumerable<Order>>> GetAsync(string email, PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(string email, PaginationDTO pagination);

        Task<ActionResponse<Order>> GetAsync(int id);

        Task<ActionResponse<Order>> UpdateFullAsync(string email, OrderDTO orderDTO);

    }
}
