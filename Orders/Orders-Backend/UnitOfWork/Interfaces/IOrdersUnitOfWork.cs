using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Responses;

namespace Orders_Backend.UnitOfWork.Interfaces
{
    public interface IOrdersUnitOfWork
    {
        Task<ActionResponse<IEnumerable<Order>>> GetAsync(string email, PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(string email, PaginationDTO pagination);

        Task<ActionResponse<Order>> GetAsync(int id);

        Task<ActionResponse<Order>> UpdateFullAsync(string email, OrderDTO orderDTO);

        Task<ActionResponse<Order>> AddAsync(Order order);
    }
}