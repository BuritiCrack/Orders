using Orders_Backend.Repositories.Interfaces;
using Orders_Backend.UnitOfWork.Interfaces;
using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Responses;

namespace Orders_Backend.UnitOfWork.Implementations
{
    public class StatesUnitOfWork : GenericUnitOfWork<State>, IStatesUnitOfWork
    {
        private readonly IStatesRepository _statesRepository;

        public StatesUnitOfWork(IGenericRepository<State> repository, IStatesRepository statesRepository) : base(repository)
        {
            _statesRepository = statesRepository;
        }

        public override async Task<ActionResponse<State>> GetAsync(int id)
            => await _statesRepository.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<State>>> GetAsync()
            => await _statesRepository.GetAsync();

        public override async Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination)
            => await _statesRepository.GetAsync(pagination);

        public override Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
            => _statesRepository.GetTotalPagesAsync(pagination);
    }
}