using Orders_Backend.Repositories.Interfaces;
using Orders_Backend.UnitOfWork.Interfaces;
using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Responses;

namespace Orders_Backend.UnitOfWork.Implementations
{
    public class CountriesUnitOfWork : GenericUnitOfWork<Country>, ICountriesUnitOfWork
    {
        private readonly ICountriesRepository _countriesRepository;

        public CountriesUnitOfWork(IGenericRepository<Country> repository, ICountriesRepository countriesRepository) : base(repository)
        {
            _countriesRepository = countriesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination)
            => await _countriesRepository.GetAsync(pagination);
       
        public override async Task<ActionResponse<Country>> GetAsync(int id)
            => await _countriesRepository.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
            => await _countriesRepository.GetAsync();

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
            => await _countriesRepository.GetTotalPagesAsync(pagination);

        public async Task<IEnumerable<Country>> GetComboAsync()
            => await _countriesRepository.GetComboAsync();
    }
}
