using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos.Product;
using api.Helpers;
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
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParameters queryParameters)
        {
            try
            {
                var (totalCount, productModels) = await _productRepository.GetAllProductsAsync(queryParameters);
                var productDtos = productModels.Select(p => p.ToProductDtoFromProduct());
                return Ok(new { TotalCount = totalCount, Products = productDtos });
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateDTO productCreateDTO)
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
                var productImages = await _productRepository.AddImagesToNewProductAsync(created.ProductId, productCreateDTO.Images);

                var productDto = productModel.ToProductDtoFromProduct();
                return CreatedAtAction(nameof(GetProductById), new { id = productModel.ProductId }, productDto);
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromForm] ProductUpdateDTO productUpdateDto)
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

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var productModel = await _productRepository.DeleteProductAsync(id, userId);
                if (productModel == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }



        }
    }
}