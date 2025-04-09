using Orders_Shared.Entities;
using Orders_Shared.Responses;

namespace Orders_Backend.UnitOfWork.Interfaces
{
    public interface ICountriesUnitOfWork
    {
        Task<ActionResponse<Country>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Country>>> GetAsync();
    }
}