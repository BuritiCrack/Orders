using Orders_Backend.Repositories.Interfaces;
using Orders_Backend.UnitOfWork.Interfaces;
using Orders_Shared.Responses;

namespace Orders_Backend.UnitOfWork.Implementations
{
    public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericUnitOfWork(IGenericRepository<T> repository) 
        {
            _repository = repository;
        }
        public virtual async Task<ActionResponse<T>> AddAsync(T entity)
            => await _repository.AddAsync(entity);

        public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
            => await _repository.DeleteAsync(id);     

        public virtual async Task<ActionResponse<T>> GetAsync(int id)
            => await _repository.GetAsync(id);

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync()
            => await _repository.GetAsync();

        public async Task<ActionResponse<T>> UpdateAsync(T entity)
            => await _repository.UpdateAsync(entity);
    }
}
