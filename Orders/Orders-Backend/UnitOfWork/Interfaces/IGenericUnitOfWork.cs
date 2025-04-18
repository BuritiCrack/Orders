﻿using Orders_Shared.DTOs;
using Orders_Shared.Responses;

namespace Orders_Backend.UnitOfWork.Interfaces
{
    public interface IGenericUnitOfWork<T> where T : class
    {
        Task<ActionResponse<T>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<T>>> GetAsync();

        Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<ActionResponse<T>> AddAsync(T entity);

        Task<ActionResponse<T>> UpdateAsync(T entity);

        Task<ActionResponse<T>> DeleteAsync(int id);
    }
}