﻿using Orders_Backend.Repositories.Interfaces;
using Orders_Backend.UnitOfWork.Interfaces;
using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Responses;

namespace Orders_Backend.UnitOfWork.Implementations
{
    public class ProductsUnitOfWork : GenericUnitOfWork<Product>, IProductsUnitOfWork
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsUnitOfWork(IGenericRepository<Product> repository, IProductsRepository productsRepository)
            : base(repository)
        {
            _productsRepository = productsRepository;
        }

        public override async Task<ActionResponse<Product>> DeleteAsync(int id) 
            => await _productsRepository.DeleteAsync(id);
        public async Task<ActionResponse<ImageDTO>> AddImageAsync(ImageDTO imageDTO) 
            => await _productsRepository.AddImageAsync(imageDTO);

        public async Task<ActionResponse<ImageDTO>> RemoveLastImageAsync(ImageDTO imageDTO) 
            => await _productsRepository.RemoveLastImageAsync(imageDTO);

        public override async Task<ActionResponse<IEnumerable<Product>>> GetAsync(PaginationDTO pagination)
            => await _productsRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
            => await _productsRepository.GetTotalPagesAsync(pagination);

        public override async Task<ActionResponse<Product>> GetAsync(int id)
            => await _productsRepository.GetAsync(id);

        public async Task<ActionResponse<Product>> AddFullAsync(ProductDTO productDTO)
            => await _productsRepository.AddFullAsync(productDTO);

        public async Task<ActionResponse<Product>> UpdateFullAsync(ProductDTO productDTO)
            => await _productsRepository.UpdateFullAsync(productDTO);
    }
}