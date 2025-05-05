using Orders_Backend.Repositories.Interfaces;
using Orders_Backend.UnitOfWork.Interfaces;
using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Responses;

namespace Orders_Backend.UnitOfWork.Implementations
{
    public class CitiesUnitOfWork : GenericUnitOfWork<City>, ICitiesUnitOfWork
    {
        private readonly ICitiesRepository _citiesRepository;

        public CitiesUnitOfWork(ICitiesRepository citiesRepository, IGenericRepository<City> genericRepository) : base(genericRepository)
        {
            _citiesRepository = citiesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTO pagination)
            => await _citiesRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
            => await _citiesRepository.GetTotalPagesAsync(pagination);

        public async Task<IEnumerable<City>> GetComboAsync(int statesId)
            => await _citiesRepository.GetComboAsync(statesId);

    }
}