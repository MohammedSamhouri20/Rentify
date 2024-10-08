using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Product;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IProductRepository
    {
        Task<(int TotalCount, List<Product> Products)> GetAllProductsAsync(ProductQueryParameters queryParameters);
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product?> CreateProductAsync(Product productModel);
        Task<Product?> UpdateProductAsync(int id, ProductUpdateDTO productDto, string OwnerId);
        Task<Product?> DeleteProductAsync(int id, string OwnerId);
        Task<List<ProductImage>> AddImagesToNewProductAsync(int ProductId, List<IFormFile> images);

        Task<List<ProductImage>> AddImagesToExistProductAsync(int ProductId, List<IFormFile> images);
    }
}