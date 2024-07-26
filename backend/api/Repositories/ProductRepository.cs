using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Product;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ICategoryRepository _categoryRepository;
        public ProductRepository(ApplicationDBContext context, ICategoryRepository categoryRepository)
        {
            _dbContext = context;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products
                                .Include(p => p.Owner)
                                .Include(p => p.Category)

                                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products
                                .Include(product => product.Owner)
                                .Include(product => product.Category)
                                .FirstOrDefaultAsync(product => product.ProductId == id);
        }

        public async Task<Product?> CreateProductAsync(Product productModel)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(productModel.CategoryId);
            if (category == null || category.CategoryType == "service")
            {
                return null;
            }

            await _dbContext.Products.AddAsync(productModel);
            await _dbContext.SaveChangesAsync();
            return productModel;
        }


        public async Task<Product?> UpdateProductAsync(int id, ProductUpdateDTO productDto, string OwnerId)
        {
            var existingProduct = await _dbContext.Products.Include(product => product.CancellationPolicy)
                                                        .Include(product => product.Category)
                                                        .FirstOrDefaultAsync(product => product.ProductId == id);

            if (existingProduct == null || existingProduct.OwnerId != OwnerId)
            {
                return null;
            }

            existingProduct.Title = productDto.Title;
            existingProduct.ProductCondition = productDto.ProductCondition;
            existingProduct.Quantity = productDto.Quantity;
            existingProduct.PriceMonthly = productDto.PriceMonthly;
            existingProduct.PriceWeekly = productDto.PriceWeekly;
            existingProduct.PriceDaily = productDto.PriceDaily;
            existingProduct.Title = productDto.Title;
            existingProduct.Description = productDto.Description;
            existingProduct.CategoryId = productDto.CategoryId;
            existingProduct.AdditionalInfo = productDto.AdditionalInfo;
            if (existingProduct.CancellationPolicy != null)
            {
                existingProduct.CancellationPolicy.Refund = productDto.Refund;
                existingProduct.CancellationPolicy.PermittedDuration = productDto.PermittedDuration;
            }

            if (existingProduct.Location != null)
            {
                existingProduct.Location.Latitude = productDto.Latitude;
                existingProduct.Location.Longitude = productDto.Longitude;
            }

            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }

        public Task<Product?> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }


        public Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsByOwnerAsync(string ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

    }
}