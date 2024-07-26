using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos.Product;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var productModels = await _productRepository.GetAllProductsAsync();
                var productDtos = productModels.Select(p => p.ToProductDtoFromProduct());
                return Ok(productDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            var productModel = await _productRepository.GetProductByIdAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }
            var productDto = productModel.ToProductDtoFromProduct();
            return Ok(productDto);

        }

        // [HttpPost("{stockId}")] التأكد من فيديو 18، لأنه البرودكت تشايلد للكاتيجوري
        // public async Task<IActionResult> CreateProduct([FromRoute] int CategoryId, [FromBody] ProductCreateDTO productCreateDTO)
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO productCreateDTO)

        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var productModel = productCreateDTO.ToProductFromProductCreateDto();

                if (userId == null)
                {
                    return Unauthorized();
                }

                productModel.OwnerId = userId;

                var created = await _productRepository.CreateProductAsync(productModel);
                if (created == null)
                {
                    return BadRequest("The category does not exist");
                }

                var productDto = productModel.ToProductDtoFromProduct();
                return CreatedAtAction(nameof(GetProductById), new { id = productModel.ProductId }, productDto);
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/product/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductUpdateDTO productUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var updatedProduct = await _productRepository.UpdateProductAsync(id, productUpdateDto, userId);

                if (updatedProduct == null)
                {
                    return NotFound();
                }

                var productDto = updatedProduct.ToProductDtoFromProduct();
                return Ok(productDto);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        // // DELETE: api/product/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteProduct(int id)
        // {
        //     var product = await _productRepo.GetProductByIdAsync(id);
        //     if (product == null)
        //     {
        //         return NotFound();
        //     }

        //     await _productRepo.DeleteProductAsync(id);
        //     return NoContent();
        // }

        // // GET: api/product/owner/{ownerId}
        // [HttpGet("owner/{ownerId}")]
        // public async Task<IActionResult> GetProductsByOwner([FromRoute] string ownerId)
        // {
        //     var products = await _productRepo.GetProductsByOwnerAsync(ownerId);
        //     var productDTO = products.Select(p => p.ToProductDTO());
        //     return Ok(productDTO);
        // }

        // // GET: api/product/category/{categoryId}
        // [HttpGet("category/{categoryId}")]
        // public async Task<IActionResult> GetProductsByCategory([FromRoute] int categoryId)
        // {
        //     var products = await _productRepo.GetProductsByCategoryAsync(categoryId);
        //     var productDTO = products.Select(p => p.ToProductDTO());
        //     return Ok(productDTO);
        // }

    }
}