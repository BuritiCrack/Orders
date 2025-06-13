using Orders_Backend.Repositories.Interfaces;
using Orders_Backend.UnitOfWork.Interfaces;
using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Responses;

namespace Orders_Backend.UnitOfWork.Implementations
{
    public class OrdersUnitOfWork : GenericUnitOfWork<Order>, IOrdersUnitOfWork
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersUnitOfWork(IGenericRepository<Order> repository, IOrdersRepository ordersRepository) : base(repository)
        {
            _ordersRepository = ordersRepository;
        }
        public async Task<ActionResponse<IEnumerable<Order>>> GetAsync(string email, PaginationDTO pagination) 
            => await _ordersRepository.GetAsync(email, pagination);

        public async Task<ActionResponse<int>> GetTotalPagesAsync(string email, PaginationDTO pagination) 
            => await _ordersRepository.GetTotalPagesAsync(email, pagination);

        public async Task<ActionResponse<Order>> UpdateFullAsync(string email, OrderDTO orderDTO) 
            => await _ordersRepository.UpdateFullAsync(email, orderDTO);

        public override async Task<ActionResponse<Order>> GetAsync(int id) 
            => await _ordersRepository.GetAsync(id);
    }
}
